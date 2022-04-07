
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
  /// </summary>
  public class UKSMGTag : PositiveIntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.UKSMG;

    /// <summary>
    /// Creates a new UKSMG tag.
    /// </summary>
    public UKSMGTag() { }

    /// <summary>
    /// Creates a new UKSMG tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public UKSMGTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new UKSMG tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public UKSMGTag(int value) : base(value) { }

  }
}
