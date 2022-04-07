
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Straight Key Century Club (SKCC) member information.
  /// </summary>
  public class SKCCTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.SKCC;

    /// <summary>
    /// Creates a new SKCC tag.
    /// </summary>
    public SKCCTag() { }

    /// <summary>
    /// Creates a new SKCC tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SKCCTag(string value) : base(value) { }
  }
}
