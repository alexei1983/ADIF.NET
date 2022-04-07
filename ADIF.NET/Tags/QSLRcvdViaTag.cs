
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the means by which the QSL was received by the logging station.
  /// </summary>
  public class QSLRcvdViaTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLRcvdVia;

    /// <summary>
    /// Valid enumeration options.
    /// </summary>
    public override ADIFEnumeration Options => Values.Via;

    /// <summary>
    /// Creates a new QSL_RCVD_VIA tag.
    /// </summary>
    public QSLRcvdViaTag() { }

    /// <summary>
    /// Creates a new QSL_RCVD_VIA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLRcvdViaTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QSL_RCVD_VIA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLRcvdViaTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
