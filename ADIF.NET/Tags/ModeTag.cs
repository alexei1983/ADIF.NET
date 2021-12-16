
namespace ADIF.NET.Tags {
  public class ModeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Mode;
    public override ADIFEnumeration Options => Values.Modes;
    }
  }
