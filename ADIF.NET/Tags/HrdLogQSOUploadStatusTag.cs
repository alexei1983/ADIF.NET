using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class HRDLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    public HRDLogQSOUploadStatusTag() { }

    public HRDLogQSOUploadStatusTag(string value) : base(value) { }
  }
}
