using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic K index at the time of the QSO.
  /// </summary>
  [DisplayName("The geomagnetic K index at the time of the QSO.")]
  public class KIndexTag : NumberTag, ITag {

    public override string Name => TagNames.KIndex;
    }
  }
