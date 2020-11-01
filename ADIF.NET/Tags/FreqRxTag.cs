using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's receiving frequency in Megahertz in a split frequency QSO.
  /// </summary>
  [DisplayName("In a split frequency QSO, the logging station's receiving frequency in Megahertz.")]
  public class FreqRxTag : NumberTag, ITag {

    public override string Name => TagNames.FreqRx;
    }
  }
