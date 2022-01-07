
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the contacted station's special activity or interest group.
  /// </summary>
  public class SigTag : StringTag, ITag {

    public override string Name => TagNames.Sig;

    public SigTag() { }

    public SigTag(string value) : base(value) { }
  }
}
