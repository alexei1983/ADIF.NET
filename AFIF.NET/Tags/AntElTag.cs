using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna elevation in degrees.
  /// </summary>
  [DisplayName("The logging station's antenna elevation, in degrees.")]
  public class AntElTag : NumberTag, ITag {

    public override string Name => TagNames.AntEl;

    public override double MaxValue => 360d;
    public override double MinValue => 0d;
    public override bool AllowValuesOverMaxOnImport => true;

    public AntElTag() { }

    public AntElTag(double elevation) {
      this.SetValue(elevation);
      }

    public override void SetValue(double value) {
      importValue = value;
      base.SetValue(ConvertValue(value));
      }

    public override void SetValue(object value) {
      var convertedVal = base.ConvertValue(value);

      if (convertedVal is double doubleVal) {
        importValue = doubleVal;
        base.SetValue(this.ConvertValue(doubleVal));
        }
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }

    public override object ConvertValue(object value) {
      var convertedVal = base.ConvertValue(value);

      if (convertedVal is double doubleVal) {

        if (doubleVal > MaxValue) {
          doubleVal = doubleVal % MaxValue;
          }

        return doubleVal;
        }

      return 0d;
      }

    double? importValue;

    }
  }
