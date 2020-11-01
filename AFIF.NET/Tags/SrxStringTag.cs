using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// </summary>
  [DisplayName("")]
  public class SrxStringTag : StringTag, ITag {

    public override string Name => TagNames.SrxString;
    }
  }
