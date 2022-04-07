
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the upload status of the QSO on the QRZ.COM online service.
  /// </summary>
  public class QRZQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.QRZQSOUploadStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    /// <summary>
    /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
    /// </summary>
    public QRZQSOUploadStatusTag() { }

    /// <summary>
    /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QRZQSOUploadStatusTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new QRZCOM_QSO_UPLOAD_STATUS tag.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public QRZQSOUploadStatusTag(ADIFEnumerationValue enumValue) : base(enumValue) { }
  }
}
