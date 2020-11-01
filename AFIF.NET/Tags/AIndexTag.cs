using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic A index at the time of the QSO.
  /// </summary>
  [DisplayName("The geomagnetic A index at the time of the QSO.")]
  public class AIndexTag : NumberTag, ITag {

    public override string Name => TagNames.AIndex;
    }
  }
