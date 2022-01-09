
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's Secondary Administrative Subdivision.
  /// </summary>
  public class CntyTag : StringTag, ITag {

    public override string Name => TagNames.Cnty;

    public CntyTag() { }

    public CntyTag(string value) : base(value) { }
  }
}
