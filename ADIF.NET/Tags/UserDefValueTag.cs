
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field along with its value.
  /// </summary>
  public class UserDefValueTag : Tag<object> {

    public override string Name => field?.FieldName ?? string.Empty;

    public int FieldId => field?.FieldId ?? -1;

    public string DataType => field?.DataType ?? string.Empty;

    public override bool RestrictOptions => field?.CustomOptions?.Length > 0;

    public override ADIFEnumeration Options => ADIFEnumeration.FromUserDefinedTag(field);

    public double MinValue => field?.LowerBound ?? 0;

    public double MaxValue => field?.UpperBound ?? 0;

    public UserDefValueTag(UserDefTag field) {
      this.field = field;
      }

    public override bool ValidateValue(object value) {

      if (base.ValidateValue(value)) {

        if (Options?.Count > 0 && RestrictOptions) {
          return Options.IsValid(value.ToString());
          }

        else if (MaxValue > MinValue) {
          var dblVal = value.ToDouble();
          return dblVal >= MinValue && dblVal <= MaxValue;
          }
        }

      return true;
      }

    UserDefTag field;

    }
  }
