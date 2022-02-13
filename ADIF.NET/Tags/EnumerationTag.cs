
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is either free-form text or selected 
  /// from a list of options.
  /// </summary>
  public class EnumerationTag : Tag<string>, ITag {

    public override IADIFType ADIFType => new ADIFEnumerationType();

    public EnumerationTag() { }

    public EnumerationTag(string value) : base(value) { }

    }
  }
