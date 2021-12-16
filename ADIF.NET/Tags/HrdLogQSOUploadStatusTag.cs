using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class HrdLogQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.QSOUploadStatuses;
    }
  }
