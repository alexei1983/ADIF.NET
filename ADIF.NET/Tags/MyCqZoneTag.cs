
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's CQ Zone.
  /// </summary>
  public class MyCQZoneTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyCQZone;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override int MinValue => 1;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override int MaxValue => 40;

    /// <summary>
    /// Creates a new MY_CQ_ZONE tag.
    /// </summary>
    public MyCQZoneTag() { }

    /// <summary>
    /// Creates a new MY_CQ_ZONE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyCQZoneTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new MY_CQ_ZONE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyCQZoneTag(int value) : base(value) { }
  }
}
