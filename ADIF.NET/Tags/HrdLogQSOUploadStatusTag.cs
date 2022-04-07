
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the upload status of the QSO on the HRDLog.net online service.
  /// </summary>
  public class HRDLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.HrdLogQSOUploadStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    /// <summary>
    /// Creates a new HRDLOG_QSO_UPLOAD_STATUS tag.
    /// </summary>
    public HRDLogQSOUploadStatusTag() { }

    /// <summary>
    /// Creates a new HRDLOG_QSO_UPLOAD_STATUS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public HRDLogQSOUploadStatusTag(string value) : base(value) { }
  }
}
