
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest precedence (e.g. for ARRL sweepstakes).
  /// </summary>
  public class PrecedenceTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Precedence;

    /// <summary>
    /// Creates a new PRECEDENCE tag.
    /// </summary>
    public PrecedenceTag() { }

    /// <summary>
    /// Creates a new PRECEDENCE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public PrecedenceTag(string value) : base(value) { }
  }
}
