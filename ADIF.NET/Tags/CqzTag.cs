
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's CQ zone.
  /// </summary>
  public class CQZTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.CQZ;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 40;

    /// <summary>
    /// Creates a new CQZ tag.
    /// </summary>
    public CQZTag() { }

    /// <summary>
    /// Creates a new CQZ tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CQZTag(double value) : base(value) { }
  }
}
