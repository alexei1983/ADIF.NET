
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the geomagnetic A index at the time of the QSO.
  /// </summary>
  public class AIndexTag : NumberTag, ITag {

    public override string Name => TagNames.AIndex;

    public AIndexTag() { }

    public AIndexTag(double value) : base(value) { }
  }
}
