
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the means by which the QSL was sent by the logging station.
  /// </summary>
  public class QSLSentViaTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSLSentVia;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.Via;

    /// <summary>
    /// Creates a new QSL_SENT_VIA tag.
    /// </summary>
    public QSLSentViaTag() { }

    /// <summary>
    /// Creates a new QSL_SENT_VIA tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLSentViaTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QSL_SENT_VIA tag.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public QSLSentViaTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
