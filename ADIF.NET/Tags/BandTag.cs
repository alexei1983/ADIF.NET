
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the band on which the QSO was made.
  /// </summary>
  public class BandTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Band;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.Bands;

    /// <summary>
    /// Creates a new BAND tag.
    /// </summary>
    public BandTag() { }

    /// <summary>
    /// Creates a new BAND tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public BandTag(string value) : base(value) { }
  }
}
