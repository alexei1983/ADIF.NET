
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
  /// </summary>
  public class UKSMGTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.UKSMG;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Creates a new UKSMG tag.
    /// </summary>
    public UKSMGTag() { }

    /// <summary>
    /// Creates a new UKSMG tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public UKSMGTag(double value) : base(value) { }

  }
}
