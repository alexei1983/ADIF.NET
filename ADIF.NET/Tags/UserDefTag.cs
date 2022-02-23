using System.Xml;
using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the definition of a user-defined QSO field.
  /// </summary>
  public class UserDefTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.UserDef;

    /// <summary>
    /// 
    /// </summary>
    public string FieldName
    {
      get { return fieldName; }

      set
      {
        if (value != null)
          UserDefHelper.ValidateFieldName(value);

        fieldName = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// 
    /// </summary>
    public override bool IsUserDef => true;

    /// <summary>
    /// 
    /// </summary>
    public new string DataType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] CustomOptions { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double LowerBound { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double UpperBound { get; set; }

    /// <summary>
    /// 
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
