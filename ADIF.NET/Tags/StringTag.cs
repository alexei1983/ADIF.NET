
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/>.
  /// </summary>
  public class StringTag : Tag<string>, ITag {

    public override IADIFType ADIFType => new ADIFString();

    public override object ConvertValue(object value) {
      return value == null ? string.Empty : value.ToString();
      }

    public override bool ValidateValue(object value) {
      return (value?.ToString() ?? string.Empty).IsASCII();
      }

    public StringTag() { }

    public StringTag(string value) : base(value) { }

    }
  }
