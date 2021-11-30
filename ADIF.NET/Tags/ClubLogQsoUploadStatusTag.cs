using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the upload status of the QSO on the Club Log online service.
  /// </summary>
  [DisplayName("The upload status of the QSO on the Club Log online service.")]
  public class ClubLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ClubLogQSOUploadStatus;
    public override string[] Options => Values.QSOUploadStatuses.GetOptions();

    }
  }
