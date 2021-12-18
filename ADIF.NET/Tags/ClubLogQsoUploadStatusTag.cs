
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the upload status of the QSO on the Club Log online service.
  /// </summary>
  public class ClubLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ClubLogQSOUploadStatus;
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    }
  }
