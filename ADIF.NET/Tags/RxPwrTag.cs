
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's transmitter power in watts.
  /// </summary>
  public class RxPwrTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.RxPwr;

    /// <summary>
    /// Minumum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new RX_PWR tag.
    /// </summary>
    public RxPwrTag() { }

    /// <summary>
    /// Creates a new RX_PWR tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public RxPwrTag(double value) : base(value) { }
  }
}
