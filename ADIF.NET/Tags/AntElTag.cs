
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna elevation in degrees.
  /// </summary>
  public class AntElTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.AntEl;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 90;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => -90;

    /// <summary>
    /// Creates a new ANT_EL tag.
    /// </summary>
    public AntElTag() { }

    /// <summary>
    /// Creates a new ANT_EL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AntElTag(double value) : base(value) { }
  }
}
