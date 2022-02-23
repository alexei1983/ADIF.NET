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
    /// 
    /// </summary>
    public virtual double MinValue => ADIFType.MinValue;

    /// <summary>
    /// 
    /// </summary>
    public virtual double MaxValue => ADIFType.MaxValue;

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => new ADIFNumber();

    /// <summary>
    /// 
    /// </summary>
    public virtual bool AllowValuesOverMaxOnImport { get; }

    /// <summary>
    /// 
    /// </summary>
    public NumberTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public NumberTag(double value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is double || value is double?)
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
            else if (MaxValue > 0 && dblVal > MaxValue)
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
