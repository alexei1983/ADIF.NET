
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's name.
  /// </summary>
  public class MyNameTag : StringTag, ITag {

    public override string Name => TagNames.MyName;

    public MyNameTag() { }

    public MyNameTag(string value) : base(value) { }
  }
}
