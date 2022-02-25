
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class TxPwrTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.TxPwr;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Creates a new TX_PWR tag.
    /// </summary>
    public TxPwrTag() { }

    /// <summary>
    /// Creates a new TX_PWR tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public TxPwrTag(double value) : base(value) { }
  }
}
