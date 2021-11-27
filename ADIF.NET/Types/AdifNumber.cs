using System;
using System.Globalization;

namespace ADIF.NET.Types {
  public class ADIFNumber : ADIFType<double?>, IFormattable {

    public override double MinValue => double.MinValue;
    public override double MaxValue => double.MaxValue;
    public override string Type => DataTypes.Number;

    public static double? Parse(string s)
    {
      var result = default(double?);

      try
      {
        var dblResult = double.Parse(s);
        result = dblResult;
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Could not convert value to {nameof(ADIFNumber)}", ex);
      }

      return result;
    }

    public static bool TryParse(string s, out double? result)
    {
      result = default(double?);

      if (string.IsNullOrEmpty(s))
        return true;

      if (double.TryParse(s, out double dblResult))
      {
        result = dblResult;
        return true;
      }

      return false;
    }

    public static bool IsValidValue(string value)
    {
      if (string.IsNullOrEmpty(value) || double.TryParse(value, out double _))
        return true;

      return false;
    }

    public static bool IsValidValue(object value)
    {

      if (value is double || value is double?)
        return true;

      return IsValidValue(value == null ? string.Empty : value.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }


    public string ToString(string format, IFormatProvider provider)
    {
      return string.Empty;
    }
  }
}
