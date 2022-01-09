
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the number of meteor scatter bursts heard by the logging station.
  /// </summary>
  public class NrBurstsTag : NumberTag, ITag {

    public override string Name => TagNames.NrBursts;

    public NrBurstsTag() { }

    public NrBurstsTag(double value) : base(value) { }
  }
}
