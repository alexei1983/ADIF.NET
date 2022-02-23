
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the distance between the logging station and the contacted 
  /// station in kilometers via the specified signal path.
  /// </summary>
  public class DistanceTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Distance;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => ADIFType.MaxValue;

    /// <summary>
    /// Creates a new DISTANCE tag.
    /// </summary>
    public DistanceTag() { }

    /// <summary>
    /// Creates a new DISTANCE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public DistanceTag(double value) : base(value) { }
  }
}
