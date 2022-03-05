
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna azimuth in degrees.
  /// </summary>
  public class AntAzTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.AntAz;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 360;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 0;

    /// <summary>
    /// Whether or not values over the maximum are allowed on import.
    /// </summary>
    public override bool AllowValuesOverMaxOnImport => true;

    /// <summary>
    /// Creates a new ANT_AZ tag.
    /// </summary>
    public AntAzTag() { }

    /// <summary>
    /// Creates a new ANT_AZ tag.
    /// </summary>
    /// <param name="azimuth">Initial tag value.</param>
    public AntAzTag(double azimuth) {

      if (azimuth > MaxValue) {
        importValue = azimuth;
        azimuth = azimuth % MaxValue;
        }

      base.SetValue(azimuth);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }

    double? importValue;
    }
  }
