using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the satellite mode.
  /// </summary>
  [DisplayName("The satellite mode.")]
  public class SatModeTag : StringTag, ITag {

    public override string Name => TagNames.SatMode;
    }
  }
