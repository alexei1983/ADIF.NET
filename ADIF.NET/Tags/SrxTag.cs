
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO received serial number.
  /// </summary>
  public class SrxTag : NumberTag, ITag {

    public override string Name => TagNames.Srx;

    public SrxTag() { }

    public SrxTag(double value) : base(value) { }
  }
}
