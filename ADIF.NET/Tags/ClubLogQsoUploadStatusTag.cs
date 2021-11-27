using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The upload status of the QSO on the Club Log online service.")]
  public class ClubLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ClubLogQSOUploadStatus;
    public override string[] Options => Values.QSOUploadStatuses.GetOptions();

    }
  }
