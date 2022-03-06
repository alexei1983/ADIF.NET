
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL sent status.
  /// </summary>
  public class QSLSentTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QSLSent;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSLSentStatuses;

    /// <summary>
    /// Creates a new QSL_SENT tag.
    /// </summary>
    public QSLSentTag() { }

    /// <summary>
    /// Creates a new QSL_SENT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLSentTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QSL_SENT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLSentTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
