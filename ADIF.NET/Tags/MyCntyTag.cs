
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's Secondary Administrative Subdivision.
  /// </summary>
  public class MyCntyTag : StringTag, ITag {

    public override string Name => TagNames.MyCnty;

    public MyCntyTag() { }

    public MyCntyTag(string value) : base(value) { }
  }
}
