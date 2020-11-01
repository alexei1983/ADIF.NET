using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's IOTA Island Identifier.
  /// </summary>
  [DisplayName("The contacted station's IOTA Island Identifier.")]
  public class IotaIslandIdTag : StringTag, ITag {

    public override string Name => TagNames.IotaIslandId;
    }
  }
