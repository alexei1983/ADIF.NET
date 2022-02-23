
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's age in years.
  /// </summary>
  public class AgeTag : NumberTag, ITag {

    public override double MinValue => 0;

    public override double MaxValue => 120;

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Age;

    /// <summary>
    /// Creates a new AGE tag.
    /// </summary>
    public AgeTag() { }

    /// <summary>
    /// Creates a new AGE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AgeTag(double value) : base(value) { }
  }
}
