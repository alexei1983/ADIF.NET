
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest class (e.g. for ARRL Field Day).
  /// </summary>
  public class ClassTag : StringTag, ITag {

    public override string Name => TagNames.Class;

    public ClassTag() { }

    public ClassTag(string value) : base(value) { }
  }
}
