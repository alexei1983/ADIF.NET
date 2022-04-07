
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents information associated with the contacted station's activity or interest group.
  /// </summary>
  public class SigInfoTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.SigInfo;

    /// <summary>
    /// Creates a new SIG_INFO tag.
    /// </summary>
    public SigInfoTag() { }

    /// <summary>
    /// Creates a new SIG_INFO tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SigInfoTag(string value) : base(value) { }
  }
}
