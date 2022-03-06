
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the upload status of the QSO on the Club Log online service.
  /// </summary>
  public class ClubLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.ClubLogQSOUploadStatus;

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    /// <summary>
    /// Creates a new CLUBLOG_QSO_UPLOAD_STATUS tag.
    /// </summary>
    public ClubLogQSOUploadStatusTag() { }

    /// <summary>
    /// Creates a new CLUBLOG_QSO_UPLOAD_STATUS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ClubLogQSOUploadStatusTag(string value) : base(value) { }
  }
}
