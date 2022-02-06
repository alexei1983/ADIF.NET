using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the value is either <see cref="true"/> or <see cref="false"/>.
  /// </summary>
  public class BooleanTag : Tag<bool?>, ITag {

    public override string TextValue
    {
      get
      {
        return Value.HasValue && Value.Value ? "Y" : 
               Value.HasValue && !Value.Value ? "N" : 
               string.Empty;
      }
    }

    public override ADIFEnumeration Options => ADIFType.Options;

    public override IADIFType ADIFType => new ADIFBoolean();

    public BooleanTag() { }

    public BooleanTag(bool value) : base(value) { }

    public override object ConvertValue(object value) {
      if (value is bool boolVal)
        return boolVal;
      else if (ADIFBoolean.TryParse(value == null ? string.Empty : value.ToString(), out bool? result))
        return result.Value;
      else
        return null;
      }
    }
  }
