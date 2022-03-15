
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class RegionTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Region;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.Regions;

    /// <summary>
    /// Creates a new REGION tag.
    /// </summary>
    public RegionTag() { }

    /// <summary>
    /// Creates a new REGION tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public RegionTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new REGION tag.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public RegionTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
