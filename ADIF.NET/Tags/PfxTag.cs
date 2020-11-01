using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's WPX prefix.
  /// </summary>
  [DisplayName("The contacted station's WPX prefix.")]
  public class PfxTag : StringTag, ITag {

    public override string Name => TagNames.Pfx;
    }
  }
