using System;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Time ADIF type.
  /// </summary>
  public class ADIFTime : ADIFType<DateTime> {

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Time;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.Time;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static DateTime Parse(string s)
    {
      if (!FromString(s, out DateTime result))
        throw new ArgumentException($"Invalid string value: '{s ?? string.Empty}'");

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out DateTime result)
    {
      return FromString(s, out result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(object value)
    {
      if (value is DateTime || value is DateTime?)
        return true;

      return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(string value)
    {
      return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    static bool FromString(string s, out DateTime result)
    {
      var success = false;

      s = s ?? string.Empty;

      success = DateTime.TryParseExact(s,
                                       s.Length > 4 ? Values.ADIF_TIME_FORMAT_LONG : Values.ADIF_TIME_FORMAT_SHORT, 
                                       CultureInfo.CurrentCulture, 
                                       DateTimeStyles.NoCurrentDateDefault,
                                       out result);

      return success;
    }
  }
}
