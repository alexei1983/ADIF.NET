using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the contacted station's equipment.
  /// </summary>
  [DisplayName("The description of the contacted station's equipment.")]
  public class RigTag : StringTag, ITag {

    public override string Name => TagNames.Rig;
    }
  }
