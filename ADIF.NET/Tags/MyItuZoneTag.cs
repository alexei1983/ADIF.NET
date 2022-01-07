
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's ITU zone.
  /// </summary>
  public class MyITUZoneTag : NumberTag, ITag {

    public override string Name => TagNames.MyITUZone;

    public override double MinValue => 1;

    public override double MaxValue => 90;

    public MyITUZoneTag() { }

    public MyITUZoneTag(double value) : base(value) { }
    }
  }
