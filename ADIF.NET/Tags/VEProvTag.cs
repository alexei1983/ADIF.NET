using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DeprecatedTag(TagNames.State)]
  public class VEProvTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.VEProv;

    /// <summary>
    /// Creates a new VE_PROV tag.
    /// </summary>
    public VEProvTag() { }

    /// <summary>
    /// Creates a new VE_PROV tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public VEProvTag(string value) : base(value) { }
  }
}
