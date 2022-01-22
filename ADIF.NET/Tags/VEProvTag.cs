using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DeprecatedTag("")]
  public class VEProvTag : StringTag, ITag {

    public override string Name => TagNames.VEProv;

    public VEProvTag() { }

    public VEProvTag(string value) : base(value) { }
  }
}
