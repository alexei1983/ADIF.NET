using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  public class EQslReceivedStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQslReceivedStatus;

    [DefaultValue("N")]
    public override string[] Options => typeof(EQslReceivedStatus).GetValuesArray();
    }
  }
