using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class SilentKeyTag : BooleanTag, ITag {

    public override string Name => TagNames.SilentKey;
    }
  }
