using System.Xml;
using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field and its value.
  /// </summary>
  public class UserDefValueTag : Tag<object> {

    public override string Name => field?.FieldName ?? string.Empty;

    public int FieldId => field?.FieldId ?? -1;

    public override string DataType => field?.DataType ?? string.Empty;

    public override bool IsUserDef => true;

    public override bool RestrictOptions => field?.CustomOptions?.Length > 0;

    public override ADIFEnumeration Options => ADIFEnumeration.FromUserDefinedTag(field);

    public double MinValue => field?.LowerBound ?? 0;

    public double MaxValue => field?.UpperBound ?? 0;

    public UserDefValueTag(UserDefTag field, object value) : base(value)
    {
      this.field = field;
    }

    public UserDefValueTag(UserDefTag field) {
      this.field = field;
      }

    public override object ConvertValue(object value)
    {
      return !(value is null) ? UserDefHelper.ConvertValueByType(value, DataType) : null;
    }

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
