
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the maximum length of meteor scatter bursts heard by the logging station, in seconds.
  /// </summary>
  public class MaxBurstsTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MaxBursts;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Creates a new MAX_BURSTS tag.
    /// </summary>
    public MaxBurstsTag() { }

    /// <summary>
    /// Creates a new MAX_BURSTS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MaxBurstsTag(double value) : base(value) { }
  }
}
