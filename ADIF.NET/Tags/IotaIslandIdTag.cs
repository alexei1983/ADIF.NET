
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's IOTA Island Identifier.
  /// </summary>
  public class IOTAIslandIdTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.IOTAIslandId;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Creates a new IOTA_ISLAND_ID tag.
    /// </summary>
    public IOTAIslandIdTag() { }

    /// <summary>
    /// Creates a new IOTA_ISLAND_ID tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public IOTAIslandIdTag(double value) : base(value) { }
  }
}
