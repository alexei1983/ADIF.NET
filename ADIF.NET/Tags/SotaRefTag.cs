
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SOTARefTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.SOTARef;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFSOTARef();

    /// <summary>
    /// Creates a new SOTA_REF tag.
    /// </summary>
    public SOTARefTag() { }

    /// <summary>
    /// Creates a new SOTA_REF tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SOTARefTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      return ADIFSOTARef.TryParse(value is null ? string.Empty : value.ToString(), out _);
    }
  }
}
