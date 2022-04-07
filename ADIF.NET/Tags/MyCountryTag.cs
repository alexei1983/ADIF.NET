
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's DXCC entity name.
  /// </summary>
  public class MyCountryTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyCountry;

    /// <summary>
    /// Creates a new MY_COUNTRY tag.
    /// </summary>
    public MyCountryTag() { }

    /// <summary>
    /// Creates a new MY_COUNTRY tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyCountryTag(string value) : base(value) { }
  }
}
