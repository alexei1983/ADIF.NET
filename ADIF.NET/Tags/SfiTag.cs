
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the solar flux at the time of the QSO.
  /// </summary>
  public class SFITag : NumberTag, ITag {

    public override string Name => TagNames.Sfi;

    public override double MaxValue => 300d;

    public override double MinValue => 0d;

    public SFITag() { }

    public SFITag(double value) : base(value) { }

  }
}
