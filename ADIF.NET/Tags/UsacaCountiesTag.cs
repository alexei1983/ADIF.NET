
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class USACACountiesTag : MultiValueStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.USACACounties;

    /// <summary>
    /// String that delimits values in a multi-valued tag.
    /// </summary>
    public override string ValueSeparator => Values.COLON.ToString();

    /// <summary>
    /// Maximum number of values allowed in a multi-valued tag.
    /// </summary>
    public override int MaxValueCount => 2;

    /// <summary>
    /// Minimum number of values allowed in a multi-valued tag.
    /// </summary>
    public override int MinValueCount => 2;

    /// <summary>
    /// Creates a new USACA_COUNTIES tag.
    /// </summary>
    public USACACountiesTag() : base() { }

    /// <summary>
    /// Creates a new USACA_COUNTIES tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public USACACountiesTag(string value) : base(value) {
    }
  }
}
