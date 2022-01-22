
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class CreditSubmittedTag : CreditListTag, ITag {

    public override string Name => TagNames.CreditSubmitted;

    public CreditSubmittedTag() { }

    public CreditSubmittedTag(string value) : base(value) { }
    }
  }
