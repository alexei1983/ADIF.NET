
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SigInfoTag : StringTag, ITag {

    public override string Name => TagNames.SigInfo;

    public SigInfoTag() { }

    public SigInfoTag(string value) : base(value) { }
  }
}
