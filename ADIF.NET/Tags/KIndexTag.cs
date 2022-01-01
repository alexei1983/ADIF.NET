
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic K index at the time of the QSO.
  /// </summary>
  public class KIndexTag : NumberTag, ITag {

    public override string Name => TagNames.KIndex;
    }
  }
