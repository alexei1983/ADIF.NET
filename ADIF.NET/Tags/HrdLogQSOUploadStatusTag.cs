using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class HrdLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    public HrdLogQSOUploadStatusTag() { }

    public HrdLogQSOUploadStatusTag(string value) : base(value) { }
  }
}
