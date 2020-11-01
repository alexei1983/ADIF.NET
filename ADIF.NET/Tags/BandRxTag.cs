using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging stastion's receiving band in a split frequency QSO.
  /// </summary>
  [DisplayName("In a split frequency QSO, the logging station's receiving band.")]
  public class BandRxTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.BandRx;

    public override string[] Options => typeof(Band).GetValuesArray();
    }
  }
