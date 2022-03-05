using System;
using System.Xml;
using ADIF.NET.Helpers;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field and its value.
  /// </summary>
  public class UserDefValueTag : Tag<object> {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => field.FieldName ?? string.Empty;

    /// <summary>
    /// Field ID.
    /// </summary>
    public int FieldId => field.FieldId;

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string DataType => field.DataType ?? string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => UserDefHelper.GetADIFType(DataType);

    /// <summary>
    /// Whether or not the tag is a user-defined tag.
    /// </summary>
    public override bool IsUserDef => true;

    /// <summary>
    /// 
    /// </summary>
    public override bool RestrictOptions => field.CustomOptions?.Length > 0;

    /// <summary>
    /// 
    /// </summary>
    public override ADIFEnumeration Options => ADIFEnumeration.FromUserDefinedTag(field);

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public double MinValue => field.LowerBound;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public double MaxValue => field.UpperBound;

    /// <summary>
    /// Creates a new user-defined QSO tag.
    /// </summary>
    /// <param name="field"><see cref="UserDefTag"/> representing the definition of the user-defined QSO tag.</param>
    /// <param name="value">Initial tag value.</param>
    public UserDefValueTag(UserDefTag field, object value) : base(value)
    {
      this.field = field ?? throw new ArgumentNullException(nameof(field), "User-defined tag definition is required.");
    }

    /// <summary>
    /// Creates a new user-defined QSO tag.
    /// </summary>
    /// <param name="field"><see cref="UserDefTag"/> representing the definition of the user-defined QSO tag.</param>
    public UserDefValueTag(UserDefTag field) {
      this.field = field ?? throw new ArgumentNullException(nameof(field), "User-defined tag definition is required.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      return !(value is null) ? UserDefHelper.ConvertValueByType(value, DataType) : null;
    }

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
    public override bool ValidateValue(object value) {

      if (base.ValidateValue(value)) {

        object convObj = null;

        try
        {
          convObj = ConvertValue(value);
        }
        catch
        {
          return false;
        }

        if (Options?.Count > 0 && RestrictOptions && convObj is string strVal) {
          return Options.IsValid(strVal);
          }
        else if (MaxValue > MinValue && (convObj is double || convObj is double?)) {
          var dblVal = (double?)convObj;
          return (dblVal.HasValue && dblVal >= MinValue && dblVal <= MaxValue) || !dblVal.HasValue;
          }
        }

      return true;
      }

    /// <summary>
    /// 
    /// </summary>
    public override XmlElement ToXml(XmlDocument document)
    {
      if (document == null)
        return null;

      var el = document.CreateElement(TagNames.UserDef);
      el.InnerText = TextValue;
      el.SetAttribute(ADXValues.ADX_FIELDNAME_ATTRIBUTE, Name);

      return el;
    }

    UserDefTag field;
    }
  }
