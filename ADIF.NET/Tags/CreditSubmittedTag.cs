
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class CreditSubmittedTag : CreditListTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.CreditSubmitted;

    /// <summary>
    /// Creates a new CREDIT_SUBMITTED tag.
    /// </summary>
    public CreditSubmittedTag() { }

    /// <summary>
    /// Creates a new CREDIT_SUBMITTED tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CreditSubmittedTag(string value) : base(value) { }
    }
  }
