
namespace ADIF.NET.Tags {

  /// <summary>
  /// Indicates whether the QSO was complete from the perspective of the logging station.
  /// </summary>
  public class QSOCompleteTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QSOComplete;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSOCompleteStatuses;

    /// <summary>
    /// Creates a new QSO_COMPLETE tag.
    /// </summary>
    public QSOCompleteTag() { }

    /// <summary>
    /// Creates a new QSO_COMPLETE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QSOCompleteTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QSO_COMPLETE tag.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public QSOCompleteTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
