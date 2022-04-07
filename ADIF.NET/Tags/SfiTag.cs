
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the solar flux at the time of the QSO.
  /// </summary>
  public class SFITag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Sfi;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 300;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Creates a new SFI tag.
    /// </summary>
    public SFITag() { }

    /// <summary>
    /// Creates a new SFI tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SFITag(double value) : base(value) { }

  }
}
