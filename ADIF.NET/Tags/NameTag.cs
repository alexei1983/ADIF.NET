
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameTag : StringTag, ITag {

    public override string Name => TagNames.Name;

    public NameTag() { }

    public NameTag(string value) : base(value) { }
    }
  }
