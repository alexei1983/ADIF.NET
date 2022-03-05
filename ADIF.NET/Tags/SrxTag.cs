
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO received serial number.
  /// </summary>
  public class SrxTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Srx;

    /// <summary>
    /// Creates a new SRX tag.
    /// </summary>
    public SrxTag() { }

    /// <summary>
    /// Creates a new SRX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SrxTag(double value) : base(value) { }
  }
}
