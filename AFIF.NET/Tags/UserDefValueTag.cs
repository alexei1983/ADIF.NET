using System;
using System.Linq;
using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field along with its value.
  /// </summary>
  [DisplayName("A user-defined QSO field along with its value.")]
  public class UserDefValueTag : Tag<object>, ITag {

    public override string Name => field?.FieldName ?? string.Empty;

    public int FieldId => field?.FieldId ?? -1;

    public string DataType => field?.DataType ?? string.Empty;

    public override bool RestrictOptions => field?.CustomOptions?.Length > 0;

    public override string[] Options => field?.CustomOptions ?? new string[] { };

    public double MinValue => field?.LowerBound ?? 0;

    public double MaxValue => field?.UpperBound ?? 0;

    public UserDefValueTag(UserDefTag field) {
      this.field = field;
      }

    public override bool ValidateValue(object value) {

      if (base.ValidateValue(value)) {

        if (Options.Length > 0 && RestrictOptions) {
          return Options.FirstOrDefault(o => o.Equals(value.ToString(),
                                                      StringComparison.OrdinalIgnoreCase)) != null;
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
