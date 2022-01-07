
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the logging station's special activity or interest group.
  /// </summary>
  public class MySigTag : StringTag, ITag {

    public override string Name => TagNames.MySig;

    public MySigTag() { }

    public MySigTag(string value) : base(value) { }
  }
}
