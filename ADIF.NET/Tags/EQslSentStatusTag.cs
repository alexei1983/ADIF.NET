using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class EQslSentStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQslSentStatus;

    [DefaultValue("N")]
    public override string[] Options => typeof(EQslSentStatus).GetValuesArray();
    }
  }
