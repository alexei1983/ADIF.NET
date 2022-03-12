using System;
using System.Collections.Generic;
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
    /// Numeric ID of the user-defined field.
    /// </summary>
    public int FieldId => field.FieldId;

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string DataType => field.DataType ?? string.Empty;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => AppUserDefHelper.GetADIFType(field.DataType);

    /// <summary>
    /// Whether or not the tag is a user-defined tag.
    /// </summary>
    public override bool IsUserDef => true;

    /// <summary>
    /// Value of the tag as a <see cref="string"/>.
    /// </summary>
    public override string TextValue
    {
      get
      {
        switch (DataType.ToUpper())
        {
          case DataTypes.Boolean:
            return Value != null && Value is bool boolVal ? boolVal ? Values.ADIF_BOOLEAN_TRUE : Values.ADIF_BOOLEAN_FALSE : string.Empty;

          case DataTypes.Date:
            return Value != null && Value is DateTime dateVal ? dateVal.ToString(Values.ADIF_DATE_FORMAT) : string.Empty;

          case DataTypes.Time:
            return Value != null && Value is DateTime timeVal ? timeVal.Second > 0 ? timeVal.ToString(Values.ADIF_TIME_FORMAT_LONG) : 
                   timeVal.Second < 1 ? timeVal.ToString(Values.ADIF_TIME_FORMAT_SHORT) : string.Empty : string.Empty;

          case DataTypes.String:
          case DataTypes.MultilineString:
          case DataTypes.IntlString:
          case DataTypes.IntlMultilineString:
            return Value != null && Value is string strVal ? strVal : string.Empty;

          case DataTypes.Enumeration:
            return Value is ADIFEnumerationValue enumVal ? enumVal.Code : Value is string enumStr ? enumStr : Value != null ? Value.ToString() : string.Empty;

          case DataTypes.CreditList:
            if (Value != null)
            {
              if (Value is CreditList creditList)
                return creditList.ToString();
              else if (Value is string creditStr)
                return creditStr;
            }
            return string.Empty;

          case DataTypes.SponsoredAwardList:
            if (Value != null)
            {
              if (Value.GetType().IsAssignableFrom(typeof(IEnumerable<string>)))
                return string.Join(Values.COMMA.ToString(), (IEnumerable<string>)Value);
              else if (Value is string awardListStr)
                return awardListStr;
            }
            return string.Empty;

          case DataTypes.Location:
            return Value is Location location ? location.ToString() : Value is string locStr ? locStr : string.Empty;

          case DataTypes.Number:
            return Value != null && Value.IsNumber() ? Value.ToString() : string.Empty;

          default:
            return Value is string genericStrVal ? genericStrVal : Value != null ? Value.ToString() : string.Empty;
        }
      }
    }

    /// <summary>
    /// Whether or not the tag value is restricted to the list of enumeration values.
    /// </summary>
    public override bool RestrictOptions => field.CustomOptions?.Length > 0;

    /// <summary>
    /// Valid enumeration values.
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
      return !(value is null) ? AppUserDefHelper.ConvertValueByType(value, DataType) : null;
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
