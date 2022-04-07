
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO transmitted serial number.
  /// </summary>
  public class StxTag : IntegerTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Stx;

    /// <summary>
    /// Creates a new STX tag.
    /// </summary>
    public StxTag() { }

    /// <summary>
    /// Creates a new STX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public StxTag(double value) : base(value) { }

    /// <summary>
    /// Creates a new STX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public StxTag(int value) : base(value) { }

    /// <summary>
    /// Creates a new STX tag.
    /// </summary>
    /// <param name="serialGenerator">Serial number generator.</param>
    public StxTag(SerialNumberGenerator serialGenerator)
    {
      if (serialGenerator != null)
        SetValue(serialGenerator.Next());
    }
  }
}
