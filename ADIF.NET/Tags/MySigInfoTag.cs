
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class MySigInfoTag : StringTag, ITag {

    public override string Name => TagNames.MySigInfo;

    public MySigInfoTag() { }

    public MySigInfoTag(string value) : base(value) { }
    }
  }
