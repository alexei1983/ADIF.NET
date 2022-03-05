using System;
using System.Xml;
using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the definition of a user QSO field.
  /// </summary>
  public class UserDefTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.UserDef;

    /// <summary>
    /// Name of the user-defined field.
    /// </summary>
    public string FieldName
    {
      get { return fieldName ?? string.Empty; }

      set
      {
        if (value != null)
          UserDefHelper.ValidateFieldName(value);

        fieldName = value;
      }
    }

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// Whether or not the tag is a user-defined tag.
    /// </summary>
    public override bool IsUserDef => true;

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public new string DataType { get; set; }

    /// <summary>
    /// Numeric ID of the user-defined field.
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// User-defined enumeration values.
    /// </summary>
    public string[] CustomOptions { get; set; }

    /// <summary>
    /// Minimum valid numeric value.
    /// </summary>
    public double LowerBound { get; set; }

    /// <summary>
    /// Maximum valid numeric value.
    /// </summary>
    public double UpperBound { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public UserDefTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldId"></param>
    /// <param name="dataType"></param>
    public UserDefTag(string fieldName, int fieldId, string dataType)
    {
      FieldName = fieldName;
      FieldId = fieldId;
      DataType = dataType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldId"></param>
    /// <param name="options"></param>
    public UserDefTag(string fieldName, int fieldId, params string[] options) : this(fieldName, fieldId, DataTypes.Enumeration)
    {
      CustomOptions = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fieldId"></param>
    /// <param name="upperBound"></param>
    /// <param name="lowerBound"></param>
    public UserDefTag(string fieldName, int fieldId, double upperBound, double lowerBound) : this(fieldName, fieldId, DataTypes.Number)
    {
      if (upperBound < lowerBound)
        throw new ArgumentException("Upper bound numeric value cannot be less than lower bound numeric value.");

      UpperBound = upperBound;
      LowerBound = lowerBound;
    }

    /// <summary>
    /// Retrieves the <see cref="XmlElement"/> representation of the current tag.
    /// <paramref name="document">XML document object.</paramref>
    /// </summary>
    public override XmlElement ToXml(XmlDocument document)
    {
      if (document == null)
        return null;

      var el = document.CreateElement(Name);
      el.InnerText = FieldName;
      el.SetAttribute(ADXValues.ADX_FIELDID_ATTRIBUTE, FieldId.ToString());

      if (!string.IsNullOrEmpty(DataType))
        el.SetAttribute(ADXValues.ADX_TYPE_ATTRIBUTE, DataType);

      if (CustomOptions != null)
      {
        var enumStr = string.Empty;
        for (var x = 0; x < CustomOptions.Length; x++)
        {
          enumStr += CustomOptions[x];
          if ((x + 1) < CustomOptions.Length)
            enumStr += Values.COMMA.ToString();
        }
        el.SetAttribute(ADXValues.ADX_ENUM_ATTRIBUTE,
                        $"{Values.CURLY_BRACE_OPEN}{enumStr}{Values.CURLY_BRACE_CLOSE}");
      }

      if (LowerBound < UpperBound)
        el.SetAttribute(ADXValues.ADX_RANGE_ATTRIBUTE, 
                        $"{Values.CURLY_BRACE_OPEN}{LowerBound}{Values.COLON}{UpperBound}{Values.CURLY_BRACE_CLOSE}");

      return el;
    }

    string fieldName;
  }
}
