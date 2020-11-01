using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the satellite.
  /// </summary>
  [DisplayName("The name of the satellite.")]
  public class SatNameTag : StringTag, ITag {

    public override string Name => TagNames.SatName;
    }
  }
