
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the satellite.
  /// </summary>
  public class SatNameTag : StringTag, ITag {

    public override string Name => TagNames.SatName;
    }
  }
