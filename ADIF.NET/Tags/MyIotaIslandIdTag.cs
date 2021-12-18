using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's IOTA Island Identifier.
  /// </summary>
  [DisplayName("The logging station's IOTA Island Identifier.")]
  public class MyIotaIslandIdTag : StringTag, ITag {

    public override string Name => TagNames.MyIOTAIslandId;
    }
  }
