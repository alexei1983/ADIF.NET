using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is numeric as represented by 
  /// a value of type <see cref="double?"/>.
  /// </summary>
  public class NumberTag : Tag<double?>, ITag {

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public virtual double MinValue => ADIFType.MinValue;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public virtual double MaxValue => ADIFType.MaxValue;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFNumber();

    /// <summary>
    /// Whether or not values greater than the maximum are allowed during import.
    /// </summary>
    public virtual bool AllowValuesOverMaxOnImport { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="NumberTag"/> class.
    /// </summary>
    public NumberTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="NumberTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public NumberTag(double value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is double || value is double? || (value != null && value.GetType().IsAssignableFrom(typeof(double?))))
        return (double?)value;
      else
      {
        try
        {
          return ADIFNumber.Parse(value == null ? string.Empty : value.ToString());
        }
        catch (Exception ex)
        {
          throw new ValueConversionException(value, Name, ex);
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      if (base.ValidateValue(value))
      {
        try
        {
          var val = ConvertValue(value);

          if (val is double dblVal)
          {
            if (dblVal < MinValue)
              return false;
            else if (MaxValue != MinValue && dblVal > MaxValue)
              return false;

            return true;
          }
        }
        catch
        {
          return false;
        }
      }

      return false;
    }
  }
}
