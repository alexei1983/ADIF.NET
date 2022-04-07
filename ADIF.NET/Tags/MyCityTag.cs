
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's city.
  /// </summary>
  public class MyCityTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyCity;

    /// <summary>
    /// Creates a new MY_CITY tag.
    /// </summary>
    public MyCityTag() { }

    /// <summary>
    /// Creates a new MY_CITY tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyCityTag(string value) : base(value) { }
  }
}
