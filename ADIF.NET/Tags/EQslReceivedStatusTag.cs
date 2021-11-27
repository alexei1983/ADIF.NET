using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class EQSLReceivedStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQSLReceivedStatus;

    [DefaultValue("N")]
    public override string[] Options => Values.EQSLReceivedStatuses.GetOptions();
    }
  }
