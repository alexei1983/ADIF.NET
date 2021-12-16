using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's ITU zone.
  /// </summary>
  [DisplayName("The logging station's ITU zone.")]
  public class MyITUZoneTag : NumberTag, ITag {

    public override string Name => TagNames.MyITUZone;

    public override double MinValue => 1;

    public override double MaxValue => 90;
    }
  }
