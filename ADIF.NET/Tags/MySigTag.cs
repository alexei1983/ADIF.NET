
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the logging station's special activity or interest group.
  /// </summary>
  public class MySigTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MySig;

    /// <summary>
    /// Creates a new MY_SIG tag.
    /// </summary>
    public MySigTag() { }

    /// <summary>
    /// Creates a new MY_SIG tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MySigTag(string value) : base(value) { }
  }
}
