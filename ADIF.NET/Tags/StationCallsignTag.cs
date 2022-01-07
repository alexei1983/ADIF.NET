
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's callsign (the callsign used over the air).
  /// </summary>
  public class StationCallSignTag : StringTag, ITag {

    public override string Name => TagNames.StationCallSign;

    public StationCallSignTag() { }

    public StationCallSignTag(string value) : base(value) { }
  }
}
