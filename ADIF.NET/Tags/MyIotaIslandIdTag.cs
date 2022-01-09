
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA Island Identifier.
  /// </summary>
  public class MyIOTAIslandIdTag : StringTag, ITag {

    public override string Name => TagNames.MyIOTAIslandId;

    public MyIOTAIslandIdTag() { }

    public MyIOTAIslandIdTag(string value) : base(value) { }
  }
}
