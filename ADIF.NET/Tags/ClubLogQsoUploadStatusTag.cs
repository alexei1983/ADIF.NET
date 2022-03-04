
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
    /// 
    /// </summary>
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    /// <summary>
    /// 
    /// </summary>
    public ClubLogQSOUploadStatusTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public ClubLogQSOUploadStatusTag(string value) : base(value) { }

  }
}
