
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's ITU zone.
  /// </summary>
  public class MyITUZoneTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyITUZone;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override int MinValue => 1;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override int MaxValue => 90;

    /// <summary>
    /// Creates a new MY_ITU_ZONE tag.
    /// </summary>
    public MyITUZoneTag() { }

    /// <summary>
    /// Creates a new MY_ITU_ZONE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyITUZoneTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new MY_ITU_ZONE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyITUZoneTag(int value) : base(value) { }
  }
  }
