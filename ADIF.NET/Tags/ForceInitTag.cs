
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class ForceInitTag : BooleanTag, ITag {

    public override string Name => TagNames.ForceInit;

    public ForceInitTag() { }

    public ForceInitTag(bool value) : base(value) { }
  }
}
