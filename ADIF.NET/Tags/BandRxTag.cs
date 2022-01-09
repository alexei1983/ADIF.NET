
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging stastion's receiving band in a split frequency QSO.
  /// </summary>
  public class BandRxTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.BandRx;

    public override ADIFEnumeration Options => Values.Bands;

    public BandRxTag() { }

    public BandRxTag(string value) : base(value) { }
  }
}
