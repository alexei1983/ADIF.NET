
namespace ADIF.NET.Tags {

  /// <summary>
  /// </summary>
  public class SrxStringTag : StringTag, ITag {

    public override string Name => TagNames.SrxString;

    public SrxStringTag() { }

    public SrxStringTag(string value) : base(value) { }
  }
}
