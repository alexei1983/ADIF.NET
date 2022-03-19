using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the PositiveInteger ADIF type.
  /// </summary>
  public class ADIFPositiveInteger : ADIFType<int?> {

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => 1;

    /// <summary>
    /// Maximum numeric value.
    /// </summary>
    public override double MaxValue => int.MaxValue;

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Number;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.PositiveInteger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static int? Parse(string s)
    {
      try
      {
        if (!string.IsNullOrEmpty(s))
        {
          var intResult = int.Parse(s);
          if (intResult < 1)
            throw new Exception("Value must be greater than zero.");

          return intResult;
        }
        else
          return null;
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Could not convert value to {nameof(ADIFPositiveInteger)}", ex);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out int? result)
    {
      result = null;

      if (string.IsNullOrEmpty(s))
        return true;

      try
      {
        result = Parse(s);
        return true;
      }
      catch
      {
        result = null;
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(string value)
    {
      return string.IsNullOrEmpty(value) || (int.TryParse(value, out int intVal) && intVal > 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(object value)
    {
      if (value is int || value is int?)
        return true;

      if (value is double dblVal)
        return dblVal.IsWhole();

      return IsValidValue(value == null ? string.Empty : value.ToString());
    }
  }
}
