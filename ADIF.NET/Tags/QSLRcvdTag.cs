
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL received status.
  /// </summary>
  public class QSLRcvdTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QSLRcvd;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSLReceivedStatuses;

    /// <summary>
    /// Creates a new QSL_RCVD tag.
    /// </summary>
    public QSLRcvdTag() { }

    /// <summary>
    /// Creates a new QSL_RCVD tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSLRcvdTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QSL_RCVD tag.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public QSLRcvdTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
