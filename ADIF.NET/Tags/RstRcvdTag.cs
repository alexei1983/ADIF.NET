
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal report from the contacted station.
  /// </summary>
  public class RstRcvdTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.RstRcvd;

    /// <summary>
    /// Creates a new RST_RCVD tag.
    /// </summary>
    public RstRcvdTag() { }

    /// <summary>
    /// Creates a new RST_RCVD tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public RstRcvdTag(string value) : base(value) { }
  }
}
