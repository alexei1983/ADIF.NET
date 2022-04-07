using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySOTARefTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MySOTARef;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFSOTARef();

    /// <summary>
    /// Creates a new MY_SOTA_REF tag.
    /// </summary>
    public MySOTARefTag() { }

    /// <summary>
    /// Creates a new MY_SOTA_REF tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MySOTARefTag(string value) : base(value) { }

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
