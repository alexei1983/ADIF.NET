
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the number of meteor scatter pings heard by the logging station.
  /// </summary>
  public class NrPingsTag : NumberTag, ITag {

    public override string Name => TagNames.NrPings;

    public override double MinValue => 0;

    public NrPingsTag() { }

    public NrPingsTag(double value) : base(value) { }
  }
}
