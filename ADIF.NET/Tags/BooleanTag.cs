using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the value is either <see cref="true"/> or <see cref="false"/>.
  /// </summary>
  public class BooleanTag : Tag<bool>, ITag {

    public override ADIFEnumeration Options => new ADIFBoolean().Options;

    public override IADIFType ADIFType => new ADIFBoolean();

    public BooleanTag() { }

    public BooleanTag(bool value) : base(value) { }

    public override object ConvertValue(object value) {
      return value?.ToBoolean() ?? false;
      }
    }
  }
