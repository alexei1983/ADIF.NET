
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SilentKeyTag : BooleanTag, ITag {

    public override string Name => TagNames.SilentKey;

    public SilentKeyTag() { }

    public SilentKeyTag(bool value) : base(value) { }
  }
}
