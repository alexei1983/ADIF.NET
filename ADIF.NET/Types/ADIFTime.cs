using System;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFTime : ADIFType<DateTime> {

    public override string Type => DataTypes.Time;

    public static DateTime Parse(string s)
    {
      if (!FromString(s, out DateTime result))
        throw new ArgumentException($"Invalid string value: '{s ?? string.Empty}'");

      return result;
    }

    public static bool TryParse(string s, out DateTime result)
    {
      return FromString(s, out result);
    }

    public static bool IsValidValue(object value)
    {
      if (value is DateTime)
        return true;

      return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);  
    }

    public static bool IsValidValue(string value)
    {
      return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);
    }

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
