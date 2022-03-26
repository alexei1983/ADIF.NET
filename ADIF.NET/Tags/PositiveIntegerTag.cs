using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is numeric as represented by 
  /// a value of type nullable <see cref="int"/> greater than zero.
  /// </summary>
  public class PositiveIntegerTag : Tag<int?>, ITag {

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public virtual int MinValue => 1;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public virtual int MaxValue => int.MaxValue;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFPositiveInteger();

    /// <summary>
    /// Creates a new instance of the <see cref="PositiveIntegerTag"/> class.
    /// </summary>
    public PositiveIntegerTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="PositiveIntegerTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public PositiveIntegerTag(int value) : base(value) { }

    /// <summary>
    /// Creates a new instance of the <see cref="PositiveIntegerTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public PositiveIntegerTag(double value)
    {
      if (!value.IsWholeNumber())
        throw new ArgumentException($"Invalid {nameof(ADIFPositiveInteger)} in tag {Name}: {value}");

      SetValue(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is int || value is int? || (value != null && value.GetType().IsAssignableFrom(typeof(int?))))
        return (int?)value;
      else if (value is double dblVal && dblVal.IsWholeNumber())
        return Convert.ToInt32(dblVal);
      else
      {
        try
        {
          return ADIFPositiveInteger.Parse(value == null ? string.Empty : value.ToString());
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
      if (value is null)
        return true;

      if (value is string strVal && string.IsNullOrEmpty(strVal))
        return true;

      try
      {
        var val = ConvertValue(value);

        if (val is int intVal)
        {
          if (intVal < MinValue)
            return false;
          else if (MaxValue > MinValue && intVal > MaxValue)
            return false;

          return true;
        }
        else if (val is int?)
          return true;
      }
      catch
      {
        return false;
      }

      return false;
    }
  }
}
