using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's longitude.
  /// </summary>
  public class LonTag : LocationTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.Lon;

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    public LonTag() { }

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LonTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    /// <param name="longitude">Initial tag value.</param>
    public LonTag(decimal longitude) : base(longitude, LocationType.Longitude) { }

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    /// <param name="location">Initial tag value.</param>
    public LonTag(Location location) : base(location) { }

  }
}
