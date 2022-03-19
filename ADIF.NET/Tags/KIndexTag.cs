
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic K index at the time of the QSO.
  /// </summary>
  public class KIndexTag : IntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.KIndex;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override int MinValue => 0;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override int MaxValue => 9;

    /// <summary>
    /// Creates a new K_INDEX tag.
    /// </summary>
    public KIndexTag() { }

    /// <summary>
    /// Creates a new K_INDEX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public KIndexTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new K_INDEX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public KIndexTag(int value) : base(value) { }
  }
}
