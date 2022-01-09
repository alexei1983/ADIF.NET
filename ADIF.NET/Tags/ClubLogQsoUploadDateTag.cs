using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSO was last uploaded to the Club Log online service.
  /// </summary>
  public class ClubLogQSOUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.ClubLogQSOUploadDate;

    public ClubLogQSOUploadDateTag() { }

    public ClubLogQSOUploadDateTag(DateTime value) : base(value) { }

  }
}
