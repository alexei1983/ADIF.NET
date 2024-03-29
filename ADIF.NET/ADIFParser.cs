﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ADIF.NET.Tags;
using ADIF.NET.Exceptions;

namespace ADIF.NET {

  /// <summary>
  /// Parses a file, a stream, or plain text in the ADIF format.
  /// </summary>
  public class ADIFParser {

    IProgress<int> progress;

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFParser"/> class.
    /// </summary>
    public ADIFParser() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFParser"/> class.
    /// </summary>
    /// <param name="progress">Progress indicator.</param>
    public ADIFParser(IProgress<int> progress)
    {
      this.progress = progress;
    }

    /// <summary>
    /// Prepares the specified file for parsing.
    /// </summary>
    /// <param name="path">Full path to the ADIF file that will be parsed.</param>
    public void LoadFile(string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentException("File path is required.", nameof(path));

      data = File.ReadAllText(path);
    }

    /// <summary>
    /// Prepares the specified text for parsing.
    /// </summary>
    /// <param name="text">Text containing the ADIF data that will be parsed.</param>
    public void Load(string text)
    {
      data = text ?? string.Empty;
    }

    /// <summary>
    /// Prepares the specified stream for parsing.
    /// </summary>
    /// <param name="stream">Stream from which ADIF data will be read and parsed.</param>
    public void LoadStream(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof(stream), "Stream cannot be null.");

      if (!stream.CanRead)
        throw new ArgumentException("Stream is not readable.", nameof(stream));

      this.data = null;
      var byteLength = stream.Length;
      var bytesRead = 0;
      var bytesToRead = byteLength < 10 ? (int)byteLength : 10;
      byte[] data = new byte[byteLength];

      using (stream)
      {
        stream.Seek(0, SeekOrigin.Begin);

        do
        {
          var n = stream.Read(data, bytesRead, bytesToRead);
          bytesRead += n;
          byteLength -= n;
        } while (byteLength > 0);
      }

      if (data != null && data.Length > 0)
        this.data = Encoding.UTF8.GetString(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="userDefinedTag"></param>
    bool IsUserDefinedField(string tagName, out UserDefTag userDefinedTag)
    {
      userDefinedTag = userDefinedFields?.FirstOrDefault(u => u.FieldName.Equals(tagName,
                                                                                 StringComparison.OrdinalIgnoreCase));
      return userDefinedTag != null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="appDefinedTag"></param>
    bool IsAppDefinedField(string tagName, out AppDefTag appDefinedTag)
    {
      appDefinedTag = appDefinedFields?.FirstOrDefault(a => a.FieldName.Equals(tagName,
                                                                               StringComparison.OrdinalIgnoreCase));
      return appDefinedTag != null;
    }

    /// <summary>
    /// Reads the file, text, or stream as ADIF and returns the resulting data set.
    /// </summary>
    public ADIFDataSet Parse()
    {
      this.headers = new Dictionary<string, string>();
      this.body = new Dictionary<int, Dictionary<string, string>>();
      this.userDefinedFields = new List<UserDefTag>();
      this.appDefinedFields = new List<AppDefTag>();
      headerInternal = new ADIFHeader();

      if (string.IsNullOrWhiteSpace(this.data))
        throw new ADIFParseException("No ADIF data found.");

      Initialize();
      var qsoCount = -1;

      while (this.i < this.data.Length)
      {
        var qso = GetQSO();

        if (qso != null && qso.Count > 0)
        {
          body.Add(++qsoCount, qso);
        }
      }

      ReportProgress(true);

      var result = new ADIFDataSet
      {
        Header = headerInternal,
        QSOs = new ADIFQSOCollection()
      };

      foreach (var key in body.Keys)
      {
        var currentQso = body[key];
        var qso = new ADIFQSO();
        foreach (var entry in currentQso)
        {
          // get the tag name and build it
          var tag = TagFactory.TagFromName(entry.Key);

          if (tag == null)
          {
            if (IsUserDefinedField(entry.Key, out UserDefTag userTag))
            {
              tag = new UserDefValueTag(userTag);
            }
            else if (IsAppDefinedField(entry.Key, out AppDefTag appTag))
            {
              tag = appTag.Clone() as AppDefTag;
            }
            else
            {
              throw new ADIFParseException($"Unknown tag: {entry.Key}");
            }
          }

          if (tag != null)
          {
            //if (tag is UserDefValueTag)
            //  tag.SetValue(tag.ConvertValue(entry.Value));
            //else
            tag.SetValue(entry.Value);

            qso.Add(tag);
          }
        }

        result.QSOs.Add(qso);
      }

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    void Initialize()
    {
      // find the position of <EOH>
      var headerEndingPos = this.data.IndexOf($"{Values.TAG_OPENING}{ADIFTags.EndHeader}{Values.TAG_CLOSING}",
                                              StringComparison.OrdinalIgnoreCase);

      // if a header is not present, we can return from the method
      if (headerEndingPos < 0)
        return;
      //throw new ADIFParseException("No header ending tag was found.");

      this.i = 0;
      var tag = string.Empty;
      var valueLength = string.Empty;
      var value = string.Empty;
      var isUserFieldDef = false;
      var fieldId = -1;
      var userDefDataType = string.Empty;

      while (this.i < headerEndingPos)
      {
        // skip comments
        if (this.data[this.i] == Values.COMMENT_INDICATOR)
        {
          while (this.i < headerEndingPos)
          {
            if (this.data[this.i] == Values.NEWLINE)
            {
              lineNumber++;
              break;
            }

            this.i++;
          }
        }
        else
        {
          // find the beginning of a tag
          if (this.data[this.i] == Values.TAG_OPENING)
          {
            this.i++;

            // record the key
            while (this.i < headerEndingPos && this.data[this.i] != Values.VALUE_LENGTH_CHAR)
            {
              if (this.data[this.i] == Values.NEWLINE)
                lineNumber++;

              tag = $"{tag}{this.data[this.i]}"; //tag + this.data[this.i];
              this.i++;

              // handle user-defined field definitions
              if (tag.Equals(ADIFTags.UserDef, StringComparison.OrdinalIgnoreCase))
              {
                isUserFieldDef = true;
                var fieldNumber = string.Empty;

                // read the field ID
                while (this.i < headerEndingPos && this.data[this.i] != Values.VALUE_LENGTH_CHAR)
                {
                  if (this.data[this.i] == Values.NEWLINE)
                    lineNumber++;

                  fieldNumber = fieldNumber + this.data[this.i];
                  this.i++;
                }

                // iterate past the :
                this.i++;

                fieldId = fieldNumber.ToInt32();

                // read the value length
                while (this.i < headerEndingPos && this.data[this.i] != Values.VALUE_LENGTH_CHAR)
                {
                  if (this.data[this.i] == Values.NEWLINE)
                    lineNumber++;

                  valueLength = valueLength + this.data[this.i];
                  this.i++;
                }

                // iterate past the :
                this.i++;

                // read the data type
                while (this.i < headerEndingPos && this.data[this.i] != Values.TAG_CLOSING)
                {
                  if (this.data[this.i] == Values.NEWLINE)
                    lineNumber++;

                  userDefDataType = userDefDataType + this.data[this.i];
                  this.i++;
                }

                // iterate past the tag closing
                this.i++;
                break;
              } // end if we found a user-defined field definition
            }

            if (!isUserFieldDef)
            {
              // iterate past the :
              this.i++;

              // find out how long the value is
              while (this.i < headerEndingPos && this.data[this.i] != Values.TAG_CLOSING)
              {
                if (this.data[this.i] == Values.NEWLINE)
                  lineNumber++;

                valueLength = valueLength + this.data[this.i];
                this.i++;
              }

              // iterate past the tag closing >
              this.i++;
            }

            var len = valueLength.ToInt32();

            // copy the value into the buffer
            while (len > 0 && this.i < headerEndingPos)
            {
              if (this.data[this.i] == Values.NEWLINE)
                lineNumber++;

              value = value + this.data[this.i];
              len--;
              this.i++;
            };

            if (!isUserFieldDef)
              this.headers[tag.Trim().ToUpper()] = value;
            else
            {
              var userDefLen = value.Length;
              var x = 0;
              var fieldName = string.Empty;
              var curlyBraceVal = string.Empty;
              string[] enumVals = null;
              var min = 0d;
              var max = 0d;

              // read the field name
              while (userDefLen > 0 && value[x] != Values.COMMA)
              {
                if (this.data[this.i] == Values.NEWLINE)
                  lineNumber++;

                fieldName = fieldName + value[x];
                userDefLen--;
                x++;
              };

              while (userDefLen > 0 && value[x] != Values.CURLY_BRACE_OPEN)
              {
                if (this.data[this.i] == Values.NEWLINE)
                  lineNumber++;

                x++;
                userDefLen--;
              }

              if (userDefLen > 0 && value[x] == Values.CURLY_BRACE_OPEN)
              {
                x++; // iterate past the curly braces

                // read the value between the curly braces
                while (userDefLen > 0 && value[x] != Values.CURLY_BRACE_CLOSE)
                {
                  if (this.data[this.i] == Values.NEWLINE)
                    lineNumber++;

                  curlyBraceVal = curlyBraceVal + value[x];
                  userDefLen--;
                  x++;
                };
              }

              if (!string.IsNullOrWhiteSpace(curlyBraceVal))
              {
                // determine how to parse the optional curly brace string (e.g. as enum or range)
                if (DataTypes.Enumeration.Equals(userDefDataType, StringComparison.OrdinalIgnoreCase) || curlyBraceVal.Contains(Values.COMMA))
                {
                  // split by comma
                  enumVals = curlyBraceVal.Split(new[] { Values.COMMA }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                  // parse as range
                  if (curlyBraceVal.Contains(Values.COLON.ToString()))
                  {
                    var minMaxArray = curlyBraceVal.Split(new[] { Values.COLON }, StringSplitOptions.RemoveEmptyEntries);

                    if (minMaxArray.Length == 2)
                    {
                      min = minMaxArray[0].ToDouble();
                      max = minMaxArray[1].ToDouble();
                    }
                  }
                  else
                  {
                    throw new ADIFParseException($"Invalid range or enumeration definition in user-defined field {fieldName ?? string.Empty}.");
                  }
                }
              }

              userDefinedFields.Add(new UserDefTag()
              {
                FieldId = fieldId,
                FieldName = fieldName,
                UpperBound = max,
                LowerBound = min,
                CustomOptions = enumVals,
                DataType = userDefDataType?.ToUpper()
              });

            } // end if we are processing a user-defined field definition

            // clear our variables
            tag = string.Empty;
            valueLength = string.Empty;
            value = string.Empty;
            isUserFieldDef = false;
            userDefDataType = string.Empty;
            fieldId = -1;
          }
        }

        this.i++;
        ReportProgress();
      }

      // iterate past the <EOH> header ending tag
      this.i = headerEndingPos + 5;

      if (this.data[this.i] == Values.NEWLINE)
        lineNumber++;

      // parse the header tags into ITag objects
      foreach (var header in headers)
      {
        // get the tag name and build it
        var headerTag = TagFactory.TagFromName(header.Key);

        if (headerTag != null && headerTag.Header)
        {
          headerTag.SetValue(header.Value);
          headerInternal.Add(headerTag);
        }
      }

      // add the user-defined fields to the headers
      foreach (var userDefined in userDefinedFields)
        headerInternal.Add(userDefined);

      ReportProgress();

      if (this.i >= this.data.Length)
        throw new ADIFParseException("ADIF data contains no QSO records.");

    } // end method

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, string> GetQSO()
    {
      if (this.i >= this.data.Length)
        return null;

      var record = string.Empty;

      var end = this.data.IndexOf($"{Values.TAG_OPENING}{ADIFTags.EndRecord}{Values.TAG_CLOSING}",
                                  this.i,
                                  StringComparison.OrdinalIgnoreCase);

      if (end < 0)
      { // is this the end?
        this.i++;
        return null;
      }

      record = this.data.Substring(this.i, end - this.i);

      this.i = end + 5;

      ReportProgress();

      return GetQSOArray(record);
    } // end method

    /// <summary>
    /// 
    /// </summary>
    /// <param name="record"></param>
    /// <returns></returns>
    Dictionary<string, string> GetQSOArray(string record)
    {
      var retVal = new Dictionary<string, string>();

      if (string.IsNullOrWhiteSpace(record))
        return retVal;

      for (var a = 0; a < record.Length; a++)
      {
        if (record[a] == Values.TAG_OPENING)
        {
          var tagName = string.Empty;
          var value = string.Empty;
          var len_str = string.Empty;
          var len = 0;

          // app-defined variables
          var programId = string.Empty;
          var appFieldName = string.Empty;
          var appFieldDataType = string.Empty;
          var appFieldLength = string.Empty;
          var isAppField = false;

          a++; //go past the tag opening <

          // get the tag
          while (record[a] != Values.VALUE_LENGTH_CHAR)
          {
            tagName = tagName + record[a]; // append this char to the tag name
            a++;

            // look for app-defined tag
            if (tagName.Equals(ADIFTags.AppDef, StringComparison.OrdinalIgnoreCase))
            {
              isAppField = true;

              // read the program ID
              while (a < record.Length && record[a] != Values.UNDERSCORE)
              {
                programId = programId + record[a];
                a++;
              }

              // bypass the underscore
              if (record[a] == Values.UNDERSCORE)
                a++;

              // read the field name (until we hit a colon)
              while (a < record.Length && record[a] != Values.VALUE_LENGTH_CHAR)
              {
                appFieldName = appFieldName + record[a];
                a++;
              }

              // read the length and data type
              if (record[a] == Values.VALUE_LENGTH_CHAR)
                a++;

              while (a < record.Length && record[a] != Values.VALUE_LENGTH_CHAR && record[a] != Values.TAG_CLOSING)
              {
                appFieldLength = appFieldLength + record[a];
                a++;
              }

              len = appFieldLength.ToInt32();

              var dataTypePresent = record[a] == Values.VALUE_LENGTH_CHAR;

              // bypass unnecessary characters
              if (record[a] == Values.VALUE_LENGTH_CHAR) //|| record[a] == Values.TagClosing)
                a++;

              // read the data type, if one is present
              if (dataTypePresent)
              {
                while (a < record.Length && record[a] != Values.TAG_CLOSING)
                {
                  appFieldDataType = appFieldDataType + record[a];
                  a++;
                }
              }

              // add to the list of app-defined fields, if we haven't already
              if (!IsAppDefinedField(appFieldName, out _))
                appDefinedFields.Add(new AppDefTag()
                {
                  DataType = appFieldDataType,
                  FieldName = appFieldName,
                  ProgramId = programId
                });

              break;

            }
          }

          if (!isAppField)
          {
            a++; // iterate past the : (value separator)

            while (record[a] != Values.TAG_CLOSING && record[a] != Values.VALUE_LENGTH_CHAR)
            {
              len_str = len_str + record[a];
              a++;
            };
            if (record[a] == Values.VALUE_LENGTH_CHAR)
            {
              while (record[a] != Values.TAG_CLOSING)
              {
                a++;
              }
            }
            len = len_str.ToInt32();
          }

          // read the field value
          while (len > 0)
          {
            a++;
            value = value + record[a];
            len--;
          }

          if (isAppField)
            retVal[appFieldName.Trim()] = value;
          else
            retVal[tagName.Trim().ToUpper()] = value;
        }

        // skip comments
        if (record[a] == Values.COMMENT_INDICATOR)
        {
          while (a < record.Length)
          {
            if (record[a] == Values.NEWLINE)
            {
              break;
            }
            a++;
          }
        }
      }
      return retVal;
    }

    /// <summary>
    /// 
    /// </summary>
    void ReportProgress(bool done = false)
    {
      if (progress == null)
        return;

      var progressRaw = (double)i / data.Length * 100.0;

      if (progressRaw > 0 || done)
        progress.Report(done ? int.MaxValue : progressRaw > int.MaxValue ? int.MaxValue : (int)progressRaw);
    }

    string data;
    int i;
    int lineNumber;
    Dictionary<string, string> headers;
    Dictionary<int, Dictionary<string, string>> body;
    List<UserDefTag> userDefinedFields;
    List<AppDefTag> appDefinedFields;
    ADIFHeader headerInternal;

  } // end class

} // end namespace
