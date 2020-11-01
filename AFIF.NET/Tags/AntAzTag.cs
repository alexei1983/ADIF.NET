using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna azimuth in degrees.
  /// </summary>
  [DisplayName("The logging station's antenna azimuth, in degrees.")]
  public class AntAzTag : NumberTag, ITag {

    public override string Name => TagNames.AntAz;

    public override double MaxValue => 360d;
    public override double MinValue => 0d;
    public override bool AllowValuesOverMaxOnImport => true;

    public AntAzTag() { }

    public AntAzTag(double azimuth) {

      if (azimuth > MaxValue) {
        importValue = azimuth;
        azimuth = azimuth % MaxValue;
        }

      base.SetValue(azimuth);
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }

    double? importValue;

    }
  }
