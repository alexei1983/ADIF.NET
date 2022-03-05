using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's latitude.
  /// </summary>
  public class LatTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Lat;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFLocation();

    /// <summary>
    /// Creates a new LAT tag.
    /// </summary>
    public LatTag() { }

    /// <summary>
    /// Creates a new LAT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LatTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new LAT tag.
    /// </summary>
    /// <param name="latitude">Decimal latitude.</param>
    public LatTag(decimal latitude)
    {
      var location = ADIFLocation.FromDecimalDegrees(latitude, LocationType.Latitude);
      if (location != null)
        SetValue(location.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
