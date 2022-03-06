
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSO frequency in Megahertz.
  /// </summary>
  public class FreqTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Freq;

    /// <summary>
    /// Creates a new FREQ tag.
    /// </summary>
    public FreqTag() { }

    /// <summary>
    /// Creates a new FREQ tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public FreqTag(double value) : base(value) { }
  }
}
