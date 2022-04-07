
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Name;

    /// <summary>
    /// Creates a new NAME tag.
    /// </summary>
    public NameTag() { }

    /// <summary>
    /// Creates a new NAME tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public NameTag(string value) : base(value) { }
    }
  }
