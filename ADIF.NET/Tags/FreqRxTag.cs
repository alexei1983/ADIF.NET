
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's receiving frequency in Megahertz in a split frequency QSO.
  /// </summary>
  public class FreqRxTag : NumberTag, ITag {

    public override string Name => TagNames.FreqRx;

    public FreqRxTag() { }

    public FreqRxTag(double value) : base(value) { }
  }
}
