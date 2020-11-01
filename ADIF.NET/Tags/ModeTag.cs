
namespace ADIF.NET.Tags {
  public class ModeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Mode;
    public override string[] Options => typeof(Mode).GetValuesArray();
    }
  }
