
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's callsign (the callsign used over the air).
  /// </summary>
  public class StationCallSignTag : BaseCallSignTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.StationCallSign;

    /// <summary>
    /// Creates a new STATION_CALLSIGN tag.
    /// </summary>
    public StationCallSignTag() { }

    /// <summary>
    /// Creates a new STATION_CALLSIGN tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public StationCallSignTag(string value) : base(value) { }
  }
}
