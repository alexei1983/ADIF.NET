using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Integer ADIF type.
  /// </summary>
  public class ADIFInteger : ADIFType<int?> {

    /// <summary>
    /// Minimum numeric value.
    /// </summary>
    public override double MinValue => int.MinValue;

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
    public override string TypeName => DataTypeNames.Integer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static int? Parse(string s)
    {
      try
      {
        if (!string.IsNullOrEmpty(s))
          return int.Parse(s);
        else
          return null;
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Could not convert value to {nameof(ADIFInteger)}", ex);
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

      if (int.TryParse(s, out int intResult))
      {
        result = intResult;
        return true;
      }

      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(string value)
    {
      if (string.IsNullOrEmpty(value) || int.TryParse(value, out int _))
        return true;

      return false;
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
