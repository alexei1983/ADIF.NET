using System;
using System.Xml;
using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an application-defined ADIF field and value.
  /// </summary>
  public class AppDefTag : Tag<object>, ITag, ICloneable {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => $"{TagNames.AppDef}{ProgramId ?? Values.DEFAULT_PROGRAM_ID}_{FieldName ?? string.Empty}";

    /// <summary>
    /// Field name.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Program ID.
    /// </summary>
    public string ProgramId {

      get {
        return programId;
        }

      set {
        if (value != null && value.Contains(Values.UNDERSCORE.ToString()))
          throw new ArgumentException("Program ID cannot contain an underscore.");

        programId = value;
        }
      }

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public new string DataType { get; set; }

    public new void SetValue(object value)
    {
      var convertedVal = ConvertValue(value);
      base.SetValue(convertedVal);
    }

    public override object ConvertValue(object value)
    {
      return !(value is null) ? UserDefHelper.ConvertValueByType(value, DataType) : null;
    }

    /// <summary>
    /// 
    /// </summary>
    public override XmlElement ToXml(XmlDocument document)
    {
      if (document == null)
        return null;

      var el = document.CreateElement("APP");
      el.InnerText = TextValue;
      el.SetAttribute("PROGRAMID", ProgramId);
      el.SetAttribute("FIELDNAME", FieldName);

      if (!string.IsNullOrEmpty(DataType))
        el.SetAttribute("TYPE", DataType);

      return el;
    }

    string programId;
    }
  }
