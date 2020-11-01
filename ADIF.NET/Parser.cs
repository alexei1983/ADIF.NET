using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Parses a file, a stream, or plain text in the ADIF format.
  /// </summary>
  public class Parser {

    public int HeaderCount => Header?.Count ?? 0;

    public int QsoCount { get; private set; }

    public AdifHeader Header { get; private set; }

    public Parser() { 
      qsos = new List<AdifQsoCollection>();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="encoding"></param>
    public void LoadFile(string path, Encoding encoding = null) {

      if (!string.IsNullOrWhiteSpace(path)) {

        if (encoding == null)
          encoding = Encoding.ASCII;

        var fi = new FileInfo(path);

        if (fi.Exists) {
          LoadStream(fi.OpenRead(), encoding);
          }
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    public void Load(string text) {
      this.data = text ?? string.Empty;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    public void LoadStream(Stream stream, Encoding encoding = null) {

      if (stream != null && stream.CanRead) {

        if (encoding == null)
          encoding = Encoding.ASCII;
     
        var byteLength = (int)stream.Length;
        var bytesRead = 0;
        byte[] data = new byte[byteLength];

        using (stream) {

          stream.Seek(0, SeekOrigin.Begin);

          do {

            var n = stream.Read(data, bytesRead, byteLength < 10 ? byteLength : 10);
            bytesRead += n;
            byteLength -= n;
            } while (byteLength > 0);
          }

        if (data != null && data.Length > 0)
          this.data = encoding.GetString(data);
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetHeader(string key) {
      return headers.ContainsKey(key) ? headers[key] : throw new InvalidOperationException($"Header key '{key}' not found.");
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qso"></param>
    /// <returns></returns>
    public Dictionary<string, string> GetQso(int qso) {
      return body.ContainsKey(qso) ? body[qso] : throw new IndexOutOfRangeException($"QSO {qso} not found.");
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="userDefinedTag"></param>
    /// <returns></returns>
    bool IsUserDefinedField(string tagName, out UserDefTag userDefinedTag) {

      userDefinedTag = userDefinedFields?.FirstOrDefault(u => u.FieldName.Equals(tagName, 
                                                                                 StringComparison.OrdinalIgnoreCase));

      return userDefinedTag != null;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="appDefinedTag"></param>
    /// <returns></returns>
    bool IsAppDefinedField(string tagName, out AppDefTag appDefinedTag) {

      appDefinedTag = appDefinedFields?.FirstOrDefault(a => a.FieldName.Equals(tagName,
                                                                               StringComparison.OrdinalIgnoreCase));

      return appDefinedTag != null;
      }

    /// <summary>
    /// 
    /// </summary>
    public void Parse() {

      this.headers = new Dictionary<string, string>();
      this.body = new Dictionary<int, Dictionary<string, string>>();
      this.userDefinedFields = new List<UserDefTag>();
      this.appDefinedFields = new List<AppDefTag>();
      this.Header = new AdifHeader();

      if (string.IsNullOrWhiteSpace(this.data))
        throw new InvalidOperationException("No ADIF data found.");

      Initialize();
      var qsoCount = -1;

      while (this.i < this.data.Length) {

        var qso = GetQso();

        if (qso != null && qso.Count > 0) {
          body.Add(++qsoCount, qso);
          }
        }

      this.QsoCount = body.Count;
      }

    /// <summary>
    /// 
    /// </summary>
    void Initialize() {

      // find the position of <EOH>
      var headerEndingPos = this.data.IndexOf($"{Values.TagOpening}{TagNames.EndHeader}{Values.TagClosing}",
                                              StringComparison.OrdinalIgnoreCase);

      if (headerEndingPos < 0)
        throw new InvalidOperationException("No header ending tag was found.");

      this.i = 0;
      var tag = string.Empty;
      var valueLength = string.Empty;
      var value = string.Empty;
      var isUserFieldDef = false;
      var fieldId = -1;
      var userDefDataType = string.Empty;

      while (this.i < headerEndingPos) {

        // skip comments
        if (this.data[this.i] == Values.CommentIndicator) {
          while (this.i < headerEndingPos) {
            if (this.data[this.i] == Values.LineEnding) {
              break;
              }

            this.i++;
            }
          }
        else {

          // find the beginning of a tag
          if (this.data[this.i] == Values.TagOpening) {
            this.i++;
            // record the key
            while (this.i < headerEndingPos && this.data[this.i] != Values.ValueLengthChar) {
              tag = $"{tag}{this.data[this.i]}"; //tag + this.data[this.i];
              this.i++;

              // handle user-defined field definitions
              if (tag.Equals(TagNames.UserDef, StringComparison.OrdinalIgnoreCase)) {

                isUserFieldDef = true;
                var fieldNumber = string.Empty;
            
                // read the field ID
                while (this.i < headerEndingPos && this.data[this.i] != Values.ValueLengthChar) {
                  fieldNumber = fieldNumber + this.data[this.i];
                  this.i++;
                  }

                // iterate past the :
                this.i++;

                fieldId = fieldNumber.ToInt32();

                // read the value length
                while (this.i < headerEndingPos && this.data[this.i] != Values.ValueLengthChar) {
                  valueLength = valueLength + this.data[this.i];
                  this.i++;
                  }

                // iterate past the :
                this.i++;

                // read the data type
                while (this.i < headerEndingPos && this.data[this.i] != Values.TagClosing) {
                  userDefDataType = userDefDataType + this.data[this.i];
                  this.i++;
                  }

                // iterate past the tag closing
                this.i++;
                break;
                } // end if we found a user-defined field definition
              }

            if (!isUserFieldDef) {

              // iterate past the :
              this.i++;

              // find out how long the value is
              while (this.i < headerEndingPos && this.data[this.i] != Values.TagClosing) {
                valueLength = valueLength + this.data[this.i];
                this.i++;
                }

              // iterate past the tag closing >
              this.i++;
              }

            var len = valueLength.ToInt32();

            // copy the value into the buffer
            while (len > 0 && this.i < headerEndingPos) {
              value = value + this.data[this.i];
              len--;
              this.i++;
              };

            if (!isUserFieldDef)
              this.headers[tag.Trim().ToUpper()] = value;
            else {

              var userDefLen = value.Length;
              var x = 0;
              var fieldName = string.Empty;
              var curlyBraceVal = string.Empty;
              string[] enumVals = null;
              var min = 0d;
              var max = 0d;

              // read the field name
              while (userDefLen > 0 && value[x] != Values.Comma) {
                fieldName = fieldName + value[x];
                userDefLen--;
                x++;
                };

              while (userDefLen > 0 && value[x] != Values.CurlyBraceOpen) {
                x++;
                userDefLen--;
                }

              if (userDefLen > 0 && value[x] == Values.CurlyBraceOpen) {
                x++; // iterate past the curly braces

                // read the value between the curly braces
                while (userDefLen > 0 && value[x] != Values.CurlyBraceClose) {
                  curlyBraceVal = curlyBraceVal + value[x];
                  userDefLen--;
                  x++;
                  };
                }

              if (!string.IsNullOrWhiteSpace(curlyBraceVal)) {

                // determine how to parse the optional curly brace string (e.g. as enum or range)
                if (DataTypes.Enumeration.Equals(userDefDataType, StringComparison.OrdinalIgnoreCase)) {

                  // split by comma
                  enumVals = curlyBraceVal.Split(new[] { Values.Comma }, StringSplitOptions.RemoveEmptyEntries);
                  }
                else {

                  // parse as range
                  if (curlyBraceVal.Contains(Values.Colon.ToString())) {

                    var minMaxArray = curlyBraceVal.Split(new[] { Values.Colon }, StringSplitOptions.RemoveEmptyEntries);

                    if (minMaxArray.Length == 2) {
                      min = minMaxArray[0].ToDouble();
                      max = minMaxArray[1].ToDouble();
                      }
                    }
                  }
                }

              userDefinedFields.Add(new UserDefTag() { FieldId = fieldId,
                                                       FieldName = fieldName,
                                                       UpperBound = max,
                                                       LowerBound = min,
                                                       CustomOptions = enumVals,
                                                       DataType = userDefDataType?.ToUpper() });

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
        }

      // iterate past the <EOH> header ending tag
      this.i = headerEndingPos + 5;

      // parse the header tags into ITag objects
      foreach (var header in headers) {

        // get the tag name and build it
        var headerTag = TagFactory.TagFromName(header.Key);

        if (headerTag != null && headerTag.Header) {
          headerTag.SetValue(header.Value);
          Header.Add(headerTag);
          }
        }

      // add the user-defined fields to the headers
      foreach (var userDefined in userDefinedFields)
        Header.Add(userDefined);

      // this.HeaderCount = headers.Count;

      if (this.i >= this.data.Length) 
        throw new InvalidOperationException("ADIF data contains no QSO records.");

      } // end method

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, string> GetQso() {

      if (this.i >= this.data.Length)
        return null;

      var record = string.Empty;

		  var end = this.data.IndexOf($"{Values.TagOpening}{TagNames.EndRecord}{Values.TagClosing}", 
                                  this.i, 
                                  StringComparison.OrdinalIgnoreCase);

      if (end < 0) { // is this the end?
        this.i++;
        return null;
        }

		  record = this.data.Substring(this.i, end - this.i);

      this.i = end + 5;
      return GetQsoArray(record);
      } // end method

    /// <summary>
    /// 
    /// </summary>
    /// <param name="record"></param>
    /// <returns></returns>
    Dictionary<string, string> GetQsoArray(string record) {

      var retVal = new Dictionary<string, string>();

      if (string.IsNullOrWhiteSpace(record))
        return retVal;

      for (var a = 0; a < record.Length; a++) {

        if (record[a] == Values.TagOpening) {
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
          while (record[a] != Values.ValueLengthChar) {
            tagName = tagName + record[a]; // append this char to the tag name
            a++;

            // look for app-defined tag
            if (tagName.Equals(TagNames.AppDef, StringComparison.OrdinalIgnoreCase)) {

              isAppField = true;

              // read the program ID
              while (a < record.Length && record[a] != Values.Underscore) {
                programId = programId + record[a];
                a++;
                }

              // bypass the underscore
              if (record[a] == Values.Underscore)
                a++;

              // read the field name (until we hit a colon)
              while (a < record.Length && record[a] != Values.ValueLengthChar) {
                appFieldName = appFieldName + record[a];
                a++;
                }

              // read the length and data type
              if (record[a] == Values.ValueLengthChar)
                a++;

              while (a < record.Length && record[a] != Values.ValueLengthChar && record[a] != Values.TagClosing) {
                appFieldLength = appFieldLength + record[a];
                a++;
                }

              len = appFieldLength.ToInt32();

              var dataTypePresent = record[a] == Values.ValueLengthChar;

              // bypass unnecessary characters
              if (record[a] == Values.ValueLengthChar) //|| record[a] == Values.TagClosing)
                a++;

              // read the data type, if one is present
              if (dataTypePresent) {

                while (a < record.Length && record[a] != Values.TagClosing) {
                  appFieldDataType = appFieldDataType + record[a];
                  a++;
                  }
                }

              // add to the list of app-defined fields, if we haven't already
              if (!IsAppDefinedField(appFieldName, out _))
                appDefinedFields.Add(new AppDefTag() { DataType = appFieldDataType,
                                                       FieldName = appFieldName,
                                                       ProgramId = programId });

              break;

              }     
            }

          if (!isAppField) {

            a++; // iterate past the : (value separator)

            while (record[a] != Values.TagClosing && record[a] != Values.ValueLengthChar) {
              len_str = len_str + record[a];
              a++;
              };
            if (record[a] == Values.ValueLengthChar) {
              while (record[a] != Values.TagClosing) {
                a++;
                }
              }
            len = len_str.ToInt32();
            }

          // read the field value
          while (len > 0) {
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
        if (record[a] == Values.CommentIndicator) {
          while (a < record.Length) {
            if (record[a] == Values.LineEnding) {
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
    /// <returns></returns>
    public IEnumerable<AdifQsoCollection> GetQsoCollection() {

      foreach (var key in body.Keys) {
        var currentQso = body[key];
        var qso = new AdifQsoCollection();
        foreach (var entry in currentQso) {

          // get the tag name and build it
          var tag = TagFactory.TagFromName(entry.Key);

          if (tag == null) {
            if (IsUserDefinedField(entry.Key, out UserDefTag userTag)) {
              tag = new UserDefValueTag(userTag);
              }
            else if (IsAppDefinedField(entry.Key, out AppDefTag appTag)) {
              tag = appTag;
              }
            }

          if (tag != null) {
            tag.SetValue(entry.Value);
            qso.Add(tag);
            }
          }

        yield return qso;
        }
      }

    string data;
    int i;
    Dictionary<string, string> headers;
    Dictionary<int, Dictionary<string, string>> body;
    List<AdifQsoCollection> qsos;
    List<UserDefTag> userDefinedFields;
    List<AppDefTag> appDefinedFields;

    } // end class

  } // end namespace
