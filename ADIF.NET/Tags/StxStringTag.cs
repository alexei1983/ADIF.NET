
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO transmitted serial number.
  /// </summary>
  public class StxStringTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.StxString;

    /// <summary>
    /// Creates a new STX_STRING tag.
    /// </summary>
    public StxStringTag() { }

    /// <summary>
    /// Creates a new STX_STRING tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public StxStringTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new STX_STRING tag.
    /// </summary>
    /// <param name="serialGenerator">Serial number generator.</param>
    public StxStringTag(SerialNumberGenerator serialGenerator)
    {
      if (serialGenerator != null)
        SetValue(serialGenerator.NextString());
    }
  }
}
