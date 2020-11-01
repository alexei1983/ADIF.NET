using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's ITU zone.
  /// </summary>
  [DisplayName("The contacted station's ITU zone.")]
  public class ItuzTag : NumberTag, ITag {

    public override string Name => TagNames.Ituz;

    public override double MinValue => 1;

    public override double MaxValue => 3;
    }
  }
