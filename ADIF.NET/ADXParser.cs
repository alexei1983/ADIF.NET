using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public class ADXParser {

    string data;

    public ADXParser()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="encoding"></param>
    public void LoadFile(string path)
    {
      if (!string.IsNullOrWhiteSpace(path))
      {
        data = File.ReadAllText(path);
      }
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
    /// 
    /// </summary>
    public ADIFDataSet Parse()
    {
      var dataSet = new ADIFDataSet
      {
        Header = new ADIFHeader(),
        QSOs = new ADIFQSOCollection()
      };

      var doc = XDocument.Parse(data);

      XElement root = doc.Root;
      XNamespace ns = root.GetDefaultNamespace();

      foreach (var headerElement in doc.Descendants(ns + ADXValues.ADX_HEADER_ELEMENT)?.Elements())
      {
        var headerTag = TagFactory.TagFromName(headerElement.Name.LocalName);

        if (headerTag == null)
          continue;

        if (headerTag is UserDefTag userDefTag)
        {
          userDefTag.FieldName = headerElement.Value;

          XAttribute fieldIdAttr = headerElement.Attribute(ADXValues.ADX_FIELDID_ATTRIBUTE);
          if (fieldIdAttr != null)
          {
            if (!int.TryParse(fieldIdAttr.Value, out int fieldId))
              throw new Exception("Invalid user-defined field ID.");

            userDefTag.FieldId = fieldId;
          }

          // get the data type indicator
          XAttribute typeAttr = headerElement.Attribute(ADXValues.ADX_TYPE_ATTRIBUTE);

          if (typeAttr != null)
            userDefTag.DataType = typeAttr.Value;

          // get the enum attribute, if present
          XAttribute enumAttr = headerElement.Attribute(ADXValues.ADX_ENUM_ATTRIBUTE);

          if (enumAttr != null)
          {
            var enumStr = (enumAttr.Value ?? string.Empty).Trim(Values.CURLY_BRACE_OPEN).Trim(Values.CURLY_BRACE_CLOSE);
            var customOptions = enumStr.Split(Values.COMMA);
            if (customOptions != null && customOptions.Length > 0)
              userDefTag.CustomOptions = customOptions;
          }

          // get the range attribute, if present
          XAttribute rangeAttr = headerElement.Attribute(ADXValues.ADX_RANGE_ATTRIBUTE);

          if (rangeAttr != null)
          {
            var rangeStr = (rangeAttr.Value ?? string.Empty).Trim(Values.CURLY_BRACE_OPEN).Trim(Values.CURLY_BRACE_CLOSE);
            var ranges = rangeStr.Split(Values.COLON);
            if (ranges != null && ranges.Length == 2)
            {
              if (!double.TryParse(ranges[0], out double lowerBound))
                throw new Exception("Invalid lower bound numeric value for user-defined field.");

              if (!double.TryParse(ranges[1], out double upperBound))
                throw new Exception("Invalid upper bound numeric value for user-defined field.");

              userDefTag.LowerBound = lowerBound;
              userDefTag.UpperBound = upperBound;
            }
          }

          dataSet.Header.Add(userDefTag);
        }
        else
        {
          headerTag.SetValue(headerElement.Value);
          dataSet.Header.Add(headerTag);
        }
      }

      foreach (var recordElement in doc.Descendants(ns + ADXValues.ADX_RECORDS_ELEMENT)?.Elements())
      {
        var qso = new ADIFQSO();

        foreach (var qsoElement in recordElement.Elements())
        {
          var qsoTag = TagFactory.TagFromName(qsoElement.Name.LocalName);

          if (qsoTag == null)
          {
            if (ADXValues.ADX_APP_ELEMENT.Equals(qsoElement.Name.LocalName))
            {
              // <APP PROGRAMID="MONOLOG" FIELDNAME="Compression" TYPE="s">off</APP>
              var appTag = new AppDefTag();

              // get the data type attribute, if present
              XAttribute typeAttr = qsoElement.Attribute(ADXValues.ADX_TYPE_ATTRIBUTE);

              if (typeAttr != null)
                appTag.DataType = typeAttr.Value;

              // get the field name attribute, if present
              XAttribute fieldNameAttr = qsoElement.Attribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE);

              if (fieldNameAttr != null)
                appTag.FieldName = fieldNameAttr.Value;

              // get the program ID attribute, if present
              XAttribute progIdAttr = qsoElement.Attribute(ADXValues.ADX_PROGRAMID_ATTRIBUTE);

              if (progIdAttr != null)
                appTag.ProgramId = progIdAttr.Value;

              appTag.SetValue(qsoElement.Value);
              qso.Add(appTag);
            }
            else if (TagNames.UserDef.Equals(qsoElement.Name.LocalName))
            {
              // <USERDEF FIELDNAME="EPC">32123</USERDEF>
              XAttribute userDefFieldNameAttr = qsoElement.Attribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE);

              if (userDefFieldNameAttr != null)
              {
                var userDefHeaderTag = dataSet.Header.GetUserDefinedTag(userDefFieldNameAttr.Value);
                if (userDefHeaderTag != null)
                {
                  qsoTag = new UserDefValueTag(userDefHeaderTag);
                  qsoTag.SetValue(qsoElement.Value);
                  qso.Add(qsoTag);
                }
              }
            }
          }
          else
          {
            qsoTag.SetValue(qsoElement.Value);
            qso.Add(qsoTag);
          }
        }

        if (qso.Count > 0)
          dataSet.QSOs.Add(qso);
      }


      return dataSet;
    }
  }
}