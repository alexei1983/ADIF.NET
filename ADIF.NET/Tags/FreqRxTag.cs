
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's receiving frequency in Megahertz in a split frequency QSO.
  /// </summary>
  public class FreqRxTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.FreqRx;

    /// <summary>
    /// Creates a new FREQ_RX tag.
    /// </summary>
    public FreqRxTag() { }

    /// <summary>
    /// Creates a new FREQ_RX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public FreqRxTag(double value) : base(value) { }
  }
}
