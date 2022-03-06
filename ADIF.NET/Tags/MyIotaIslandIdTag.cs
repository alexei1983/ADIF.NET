
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA Island Identifier.
  /// </summary>
  public class MyIOTAIslandIdTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyIOTAIslandId;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new MY_IOTA_ISLAND_ID tag.
    /// </summary>
    public MyIOTAIslandIdTag() { }

    /// <summary>
    /// Creates a new MY_IOTA_ISLAND_ID tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyIOTAIslandIdTag(double value) : base(value) { }
  }
}
