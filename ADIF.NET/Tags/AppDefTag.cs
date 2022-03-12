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

    /// <summary>
    /// Creates a new application-defined field.
    /// </summary>
    public AppDefTag() { }

    /// <summary>
    /// Creates a new application-defined field.
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="programId"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public AppDefTag(string fieldName, string programId, string dataType, object value)
    {
      FieldName = fieldName;
      ProgramId = programId;
      DataType = dataType ?? string.Empty;

      if (value != null)
        SetValue(value);
    }

    /// <summary>
    /// Creates a new application-defined field.
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="programId"></param>
    /// <param name="dataType"></param>
    public AppDefTag(string fieldName, string programId, string dataType) : this(fieldName, programId, dataType, null) { }

    /// <summary>
    /// Creates a new application-defined field.
    /// </summary>
    /// <param name="fullFieldName"></param>
    /// <param name="dataType"></param>
    /// <param name="value"></param>
    public AppDefTag(string fullFieldName, string dataType, object value)
    {
      var fieldParts = AppUserDefHelper.SplitAppDefinedFieldName(fullFieldName);

      ProgramId = fieldParts[1];
      FieldName = fieldParts[2];
      DataType = dataType;

      if (value != null)
        SetValue(value);
    }

    /// <summary>
    /// Creates a new application-defined field.
    /// </summary>
    /// <param name="fullFieldName"></param>
    /// <param name="value"></param>
    public AppDefTag(string fullFieldName, object value) : this(fullFieldName, null, value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public new void SetValue(object value)
    {
      var convertedVal = ConvertValue(value);
      base.SetValue(convertedVal);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      return !(value is null) ? AppUserDefHelper.ConvertValueByType(value, DataType) : null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="document"></param>
    public override XmlElement ToXml(XmlDocument document)
    {
      if (document == null)
        return null;

      var el = document.CreateElement(ADXValues.ADX_APP_ELEMENT);
      el.InnerText = TextValue;
      el.SetAttribute(ADXValues.ADX_PROGRAMID_ATTRIBUTE, ProgramId);
      el.SetAttribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE, FieldName);

      if (!string.IsNullOrEmpty(DataType))
        el.SetAttribute(ADXValues.ADX_TYPE_ATTRIBUTE, DataType);

      return el;
    }

    string programId;
    }
  }
