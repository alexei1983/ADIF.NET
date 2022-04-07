
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's email address.
  /// </summary>
  public class EmailTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Email;

    /// <summary>
    /// Creates a new EMAIL tag.
    /// </summary>
    public EmailTag() { }

    /// <summary>
    /// Creates a new EMAIL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public EmailTag(string value) : base(value) { }
  }
}
