using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna elevation in degrees.
  /// </summary>
  public class AntElTag : NumberTag, ITag {

    public override string Name => TagNames.AntEl;

    public override double MaxValue => 360d;
    public override double MinValue => 0d;
    public override bool AllowValuesOverMaxOnImport => true;

    public AntElTag() { }

    public AntElTag(double value) : base(value) { }

    public override void SetValue(double? value) {
      importValue = value;
      SetValue(value);
      }

    public override void SetValue(object value) {
      var convertedVal = ConvertValue(value);

      if (convertedVal is double doubleVal) {
        importValue = doubleVal;
        base.SetValue(doubleVal);
        }
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }

    public override object ConvertValue(object value) {
      var convertedVal = 0d;

      if (value is double)
        convertedVal = (double)value;
      else if (ADIFNumber.TryParse(value == null ? string.Empty : value.ToString(), out double? result))
        convertedVal = result.HasValue ? result.Value : 0;

      if (convertedVal is double doubleVal) {

        if (doubleVal > MaxValue) {
          doubleVal = doubleVal % MaxValue;
          }
        else if (doubleVal < MinValue)
          doubleVal = MinValue;

        return doubleVal;
        }

      return 0d;
      }

    double? importValue;

    }
  }
