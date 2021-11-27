
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/> 
  /// with the potential presence of non-ASCII characters.
  /// </summary>
  public class IntlStringTag : Tag<string>, ITag {

    public override object ConvertValue(object value) {
      return value == null ? string.Empty : value.ToString();
      }

    public override bool ValidateValue(object value) {
      return true;
      }
    }
  }
