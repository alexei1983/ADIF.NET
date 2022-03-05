
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic A index at the time of the QSO.
  /// </summary>
  public class AIndexTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.AIndex;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 400;

    /// <summary>
    /// Creates a new A_INDEX tag.
    /// </summary>
    public AIndexTag() { }

    /// <summary>
    /// Creates a new A_INDEX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public AIndexTag(double value) : base(value) { }
  }
}
