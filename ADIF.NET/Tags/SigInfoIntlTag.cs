
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class SigInfoIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.SigInfoIntl;

    public SigInfoIntlTag() { }

    public SigInfoIntlTag(string value) : base(value) { }
  }
}
