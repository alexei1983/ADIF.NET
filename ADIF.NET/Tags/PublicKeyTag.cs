
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a public encryption key.
  /// </summary>
  public class PublicKeyTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.PublicKey;

    /// <summary>
    /// Creates a new PUBLIC_KEY tag.
    /// </summary>
    public PublicKeyTag() { }

    /// <summary>
    /// Creates a new PUBLIC_KEY tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public PublicKeyTag(string value) : base(value) { }
  }
}
