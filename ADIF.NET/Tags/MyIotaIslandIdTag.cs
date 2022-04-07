
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA Island Identifier.
  /// </summary>
  public class MyIOTAIslandIdTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyIOTAIslandId;

    /// <summary>
    /// Creates a new MY_IOTA_ISLAND_ID tag.
    /// </summary>
    public MyIOTAIslandIdTag() { }

    /// <summary>
    /// Creates a new MY_IOTA_ISLAND_ID tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyIOTAIslandIdTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new MY_IOTA_ISLAND_ID tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyIOTAIslandIdTag(int value) : base(value) { }
  }
}
