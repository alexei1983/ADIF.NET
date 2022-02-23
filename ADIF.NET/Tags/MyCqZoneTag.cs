
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's CQ Zone.
  /// </summary>
  public class MyCQZoneTag : NumberTag, ITag {

    public override string Name => TagNames.MyCQZone;

    public override double MinValue => 1;

    public override double MaxValue => 40;

    public MyCQZoneTag() { }

    public MyCQZoneTag(double value) : base(value) { }
  }
}
