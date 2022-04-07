
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the number of meteor scatter bursts heard by the logging station.
  /// </summary>
  public class NrBurstsTag : IntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.NrBursts;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override int MinValue => 0;

    /// <summary>
    /// Creates a new NR_BURSTS tag.
    /// </summary>
    public NrBurstsTag() { }

    /// <summary>
    /// Creates a new NR_BURSTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public NrBurstsTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new NR_BURSTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public NrBurstsTag(int value) : base(value) { }
  }
}
