
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class CreditGrantedTag : CreditListTag, ITag {

    public override string Name => TagNames.CreditGranted;

    public CreditGrantedTag() { }

    public CreditGrantedTag(string value) : base(value) { }
    }
  }
