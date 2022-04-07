using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSO was last uploaded to the Club Log online service.
  /// </summary>
  public class ClubLogQSOUploadDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.ClubLogQSOUploadDate;

    /// <summary>
    /// Creates a new CLUBLOG_QSO_UPLOAD_DATE tag.
    /// </summary>
    public ClubLogQSOUploadDateTag() { }

    /// <summary>
    /// Creates a new CLUBLOG_QSO_UPLOAD_DATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public ClubLogQSOUploadDateTag(DateTime value) : base(value) { }
  }
}
