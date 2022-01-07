
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSO frequency in Megahertz.
  /// </summary>
  public class FreqTag : NumberTag, ITag {

    public override string Name => TagNames.Freq;

    public FreqTag() { }

    public FreqTag(double value) : base(value) { }
  }
}
