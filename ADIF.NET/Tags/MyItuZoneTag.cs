
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's ITU zone.
  /// </summary>
  public class MyITUZoneTag : NumberTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.MyITUZone;

    /// <summary>
    /// 
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// 
    /// </summary>
    public override double MaxValue => 90;

    /// <summary>
    /// 
    /// </summary>
    public MyITUZoneTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public MyITUZoneTag(double value) : base(value) { }
    }
  }
