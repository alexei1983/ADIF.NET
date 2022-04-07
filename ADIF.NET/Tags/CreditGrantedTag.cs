
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the list of credits granted for this QSO.
  /// </summary>
  public class CreditGrantedTag : CreditListTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.CreditGranted;

    /// <summary>
    /// Creates a new CREDIT_GRANTED tag.
    /// </summary>
    public CreditGrantedTag() { }

    /// <summary>
    /// Creates a new CREDIT_GRANTED tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public CreditGrantedTag(string value) : base(value) { }
    }
  }
