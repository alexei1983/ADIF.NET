
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging stastion's receiving band in a split frequency QSO.
  /// </summary>
  public class BandRxTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.BandRx;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.Bands;

    /// <summary>
    /// Creates a new BAND_RX tag.
    /// </summary>
    public BandRxTag() { }

    /// <summary>
    /// Creates a new BAND_RX tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public BandRxTag(string value) : base(value) { }
  }
}
