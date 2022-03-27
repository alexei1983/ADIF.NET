using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ADIF.NET.Tags;
using ADIF.NET.Exceptions;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public class ADXParser {

    /// <summary>
    /// Creates a new instance of the <see cref="ADXParser"/> class.
    /// </summary>
    public ADXParser()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADXParser"/> class.
    /// </summary>
    /// <param name="progress">Progress indicator.</param>
    public ADXParser(IProgress<int> progress)
    {
      this.progress = progress;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public void LoadFile(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentException("File path is required.", nameof(path));

      data = File.ReadAllText(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    public void Load(string text)
    {
      data = text ?? string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    public void LoadStream(Stream stream)
    {
      //data = new XmlDocument();
      //data.Load(stream);
    }

    /// <summary>
    /// Parses the ADX data.
    /// </summary>
    public ADIFDataSet Parse()
    {
      var dataSet = new ADIFDataSet
      {
        Header = new ADIFHeader(),
        QSOs = new ADIFQSOCollection()
      };

      var doc = XDocument.Parse(data);

      if (doc.Root == null)
        throw new ADXParseException("No XML document root found.");

      if (doc.Root.Name.LocalName != ADXValues.ADX_ROOT_ELEMENT)
        throw new ADXParseException("Invalid ADX document.");

      elementCount = doc.Descendants().Count();

      XNamespace ns = doc.Root.GetDefaultNamespace();

      var qsoList = new List<ADIFQSO>();

      foreach (var headerElement in doc.Descendants(ns + ADXValues.ADX_HEADER_ELEMENT)?.Elements())
      {
        currentElementCount++;

        var headerTag = TagFactory.TagFromName(headerElement.Name.LocalName);

        if (headerTag == null)
          continue;

        if (!headerTag.Header)
          throw new ADXParseException($"Tag {headerTag.Name} is not a header tag.");

        if (headerTag is UserDefTag userDefTag)
        {
          if (string.IsNullOrEmpty(headerElement.Value))
            throw new ADXParseException("Field name is required for all user-defined fields.");

          userDefTag.FieldName = headerElement.Value;

          XAttribute fieldIdAttr = headerElement.Attribute(ADXValues.ADX_FIELDID_ATTRIBUTE);

          if (fieldIdAttr == null || string.IsNullOrEmpty(fieldIdAttr.Value))
            throw new ADXParseException($"No field ID specified for user-defined field {userDefTag.FieldName}.");

          // convert to integer
          if (!int.TryParse(fieldIdAttr.Value, out int fieldId))
            throw new ADXParseException($"Invalid field ID for user-defined field {userDefTag.FieldName}.");

          userDefTag.FieldId = fieldId;

          // get the data type indicator
          XAttribute typeAttr = headerElement.Attribute(ADXValues.ADX_TYPE_ATTRIBUTE);

          if (typeAttr != null)
            userDefTag.DataType = typeAttr.Value?.ToUpper();

          // get the enum attribute, if present
          XAttribute enumAttr = headerElement.Attribute(ADXValues.ADX_ENUM_ATTRIBUTE);

          if (enumAttr != null)
          {
            var enumStr = (enumAttr.Value ?? string.Empty).Trim().Trim(Values.CURLY_BRACE_OPEN).Trim(Values.CURLY_BRACE_CLOSE);
            var customOptions = enumStr.Split(Values.COMMA);
            if (customOptions != null && customOptions.Length > 0)
              userDefTag.CustomOptions = customOptions;
          }

          // get the range attribute, if present
          XAttribute rangeAttr = headerElement.Attribute(ADXValues.ADX_RANGE_ATTRIBUTE);

          if (rangeAttr != null)
          {
            var rangeStr = (rangeAttr.Value ?? string.Empty).Trim().Trim(Values.CURLY_BRACE_OPEN).Trim(Values.CURLY_BRACE_CLOSE);
            var ranges = rangeStr.Split(Values.COLON);
            if (ranges != null && ranges.Length == 2)
            {
              if (!double.TryParse(ranges[0], out double lowerBound))
                throw new ADXParseException($"Invalid lower bound numeric value for user-defined field {userDefTag.FieldName}.");

              if (!double.TryParse(ranges[1], out double upperBound))
                throw new ADXParseException($"Invalid upper bound numeric value for user-defined field {userDefTag.FieldName}.");

              userDefTag.LowerBound = lowerBound;
              userDefTag.UpperBound = upperBound;
            }
            else
              throw new ADXParseException($"Invalid range for user-defined field {userDefTag.FieldName}.");
          }

          dataSet.Header.Add(userDefTag);
        }
        else
        {
          headerTag.SetValue(headerElement.Value);
          dataSet.Header.Add(headerTag);
        }
        ReportProgress();
      }

      foreach (var recordElement in doc.Descendants(ns + ADXValues.ADX_RECORDS_ELEMENT)?.Elements())
      {
        currentElementCount++;

        var qso = new ADIFQSO();
        var qsoTags = new List<ITag>();

        foreach (var qsoElement in recordElement.Elements())
        {
          currentElementCount++;

          var qsoTag = TagFactory.TagFromName(qsoElement.Name.LocalName);

          if (qsoTag == null || TagNames.UserDef.Equals(qsoElement.Name.LocalName))
          {
            if (ADXValues.ADX_APP_ELEMENT.Equals(qsoElement.Name.LocalName))
            {
              // <APP PROGRAMID="MONOLOG" FIELDNAME="Compression" TYPE="s">off</APP>
              var appTag = new AppDefTag();

              // get the data type attribute, if present
              XAttribute typeAttr = qsoElement.Attribute(ADXValues.ADX_TYPE_ATTRIBUTE);

              if (typeAttr != null)
                appTag.DataType = typeAttr.Value?.ToUpper();

              // get the field name attribute, if present
              XAttribute fieldNameAttr = qsoElement.Attribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE);

              if (fieldNameAttr == null || string.IsNullOrEmpty(fieldNameAttr.Value))
                throw new ADXParseException("Field name is required for all application-defined fields.");

              appTag.FieldName = fieldNameAttr.Value;

              // get the program ID attribute, if present
              XAttribute progIdAttr = qsoElement.Attribute(ADXValues.ADX_PROGRAMID_ATTRIBUTE);

              if (progIdAttr == null || string.IsNullOrEmpty(progIdAttr.Value))
                throw new ADXParseException($"Program ID is required for application-defined field {appTag.FieldName}");

              appTag.ProgramId = progIdAttr.Value;

              appTag.SetValue(qsoElement.Value);
              qsoTags.Add(appTag);
            }
            else if (TagNames.UserDef.Equals(qsoElement.Name.LocalName))
            {
              // <USERDEF FIELDNAME="EPC">32123</USERDEF>
              XAttribute userDefFieldNameAttr = qsoElement.Attribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE);

              if (userDefFieldNameAttr == null || string.IsNullOrEmpty(userDefFieldNameAttr.Value))
                throw new ADXParseException("Field name is required for all user-defined fields.");

              var userDefHeaderTag = dataSet.Header.GetUserDefinedTag(userDefFieldNameAttr.Value);

              if (userDefHeaderTag == null)
                throw new ADXParseException($"No user-defined field was found with name {userDefFieldNameAttr.Value}");

              qsoTag = new UserDefValueTag(userDefHeaderTag);
              qsoTag.SetValue(qsoElement.Value);
              qsoTags.Add(qsoTag);
            }
          }
          else
          {
            qsoTag.SetValue(qsoElement.Value);
            qsoTags.Add(qsoTag);
          }

          ReportProgress();
        }

        if (qsoTags.Count > 0)
        {
          qso.AddRange(qsoTags.ToArray());
          qsoList.Add(qso);
        }
      }

      if (qsoList.Count > 0)
        dataSet.QSOs.AddRange(qsoList.ToArray());

      ReportProgress(true);

      return dataSet;
    }

    /// <summary>
    /// 
    /// </summary>
    void ReportProgress(bool done = false)
    {
      if (progress == null)
        return;

      var progressRaw = (double)currentElementCount / elementCount * 100.0;

      if (progressRaw > 0 || done)
        progress.Report(done ? int.MaxValue : progressRaw > int.MaxValue ? int.MaxValue : (int)progressRaw);
    }

    IProgress<int> progress;
    string data;
    int elementCount;
    int currentElementCount;
  }
}