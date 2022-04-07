using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the date the QSO was last uploaded to the HRDLog.net online service.
  /// </summary>
  public class HRDLogQSOUploadDateTag : DateTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.HrdLogQSOUploadDate;

    /// <summary>
    /// Creates a new HRDLOG_QSO_UPLOAD_DATE tag.
    /// </summary>
    public HRDLogQSOUploadDateTag() { }

    /// <summary>
    /// Creates a new HRDLOG_QSO_UPLOAD_DATE tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public HRDLogQSOUploadDateTag(DateTime value) : base(value) { }
  }
}
