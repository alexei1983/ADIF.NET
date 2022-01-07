
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's IOTA Island Identifier.
  /// </summary>
  public class IOTAIslandIdTag : StringTag, ITag {

    public override string Name => TagNames.IOTAIslandId;

    public IOTAIslandIdTag() { }

    public IOTAIslandIdTag(string value) : base(value) { }
  }
}
