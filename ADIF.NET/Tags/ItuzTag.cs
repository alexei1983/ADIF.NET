
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's ITU zone.
  /// </summary>
  public class ITUZTag : NumberTag, ITag {

    public override string Name => TagNames.ITUZ;

    public override double MinValue => 1;

    public override double MaxValue => 90;
    }
  }
