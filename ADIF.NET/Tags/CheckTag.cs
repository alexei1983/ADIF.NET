
namespace ADIF.NET.Tags {

  /// <summary>
  /// The contest check (e.g. for ARRL Sweepstakes).
  /// </summary>
  public class CheckTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Check;

    /// <summary>
    /// Creates a new CHECK tag.
    /// </summary>
    public CheckTag() { }

    /// <summary>
    /// Creates a new CHECK tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CheckTag(string value) : base(value) { }
  }
}
