using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class EQSLSentStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQSLSentStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.EQSLSentStatuses;
    }
  }
