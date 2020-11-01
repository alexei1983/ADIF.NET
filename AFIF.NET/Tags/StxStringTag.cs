using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// </summary>
  [DisplayName("")]
  public class StxStringTag : StringTag, ITag {

    public override string Name => TagNames.StxString;
    }
  }
