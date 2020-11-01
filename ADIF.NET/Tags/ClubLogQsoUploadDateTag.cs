using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  [DisplayName("The date the QSO was last uploaded to the Club Log online service.")]
  public class ClubLogQsoUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.ClubLogQsoUploadDate;

    }
  }
