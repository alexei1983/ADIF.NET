using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class HrdLogQSOUploadStatus : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadStatus;

    [DefaultValue("N")]
    public override string[] Options => Values.QSOUploadStatuses.GetOptions();
    }
  }
