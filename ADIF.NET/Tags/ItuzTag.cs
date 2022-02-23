
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's ITU zone.
  /// </summary>
  public class ITUZTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.ITUZ;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 90;

    /// <summary>
    /// Creates a new ITUZ tag.
    /// </summary>
    public ITUZTag() { }

    /// <summary>
    /// Creates a new ITUZ tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ITUZTag(double value) : base(value) { }
  }
}
