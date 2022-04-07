
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's owner's callsign.
  /// </summary>
  public class EqCallTag : BaseCallSignTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.EqCall;

    /// <summary>
    /// Creates a new EQ_CALL tag.
    /// </summary>
    public EqCallTag() { }

    /// <summary>
    /// Creates a new EQ_CALL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EqCallTag(string value) : base(value) { }
  }
}
