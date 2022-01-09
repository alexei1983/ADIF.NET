
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's WPX prefix.
  /// </summary>
  public class PfxTag : StringTag, ITag {

    public override string Name => TagNames.Pfx;

    public PfxTag() { }

    public PfxTag(string value) : base(value) { }
  }
}
