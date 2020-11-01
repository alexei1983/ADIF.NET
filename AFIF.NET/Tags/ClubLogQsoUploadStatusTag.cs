using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The upload status of the QSO on the Club Log online service.")]
  public class ClubLogQsoUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.ClubLogQsoUploadStatus;
    public override string[] Options => typeof(QsoUploadStatus).GetValuesArray();

    }
  }
