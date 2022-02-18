
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.Name;

    /// <summary>
    /// Creates a new instance of the NAME tag.
    /// </summary>
    public NameTag() { }

    /// <summary>
    /// Creates a new instance of the NAME tag.
    /// </summary>
    /// <param name="value">Contacted station's operator's name.</param>
    public NameTag(string value) : base(value) { }
    }
  }
