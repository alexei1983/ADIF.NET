using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's antenna elevation in degrees.
  /// </summary>
  public class AntElTag : NumberTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.AntEl;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => 90;

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => -90;

    /// <summary>
    /// Whether or not to allow values over the maximum on import.
    /// </summary>
    public override bool AllowValuesOverMaxOnImport => true;

    /// <summary>
    /// Creates a new ANT_EL tag.
    /// </summary>
    public AntElTag() { }

    /// <summary>
    /// Creates a new ANT_EL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
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
