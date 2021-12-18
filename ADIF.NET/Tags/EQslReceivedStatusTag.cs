using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class EQSLReceivedStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQSLReceivedStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.EQSLReceivedStatuses;
    }
  }
