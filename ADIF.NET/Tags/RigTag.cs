
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the contacted station's equipment.
  /// </summary>
  public class RigTag : StringTag, ITag {

    public override string Name => TagNames.Rig;
    }
  }
