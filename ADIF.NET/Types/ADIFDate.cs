using System;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Date ADIF type.
  /// </summary>
  public class ADIFDate : ADIFType<DateTime> {

    public override string Type => DataTypes.Date;

    public static DateTime Parse(string s)
    {
      if (!FromString(s, out DateTime result))
        throw new ArgumentException($"Invalid ADIF Date: '{s ?? string.Empty}'");

      return result;
    }

    public static bool TryParse(string s, out DateTime result)
    {
      return FromString(s, out result);
    }

    public static bool IsValidValue(object value)
    {
      if (value is DateTime || value is DateTime?)
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

      success = DateTime.TryParseExact(s ?? string.Empty,
                                       Values.ADIF_DATE_FORMAT, 
                                       CultureInfo.CurrentCulture, 
                                       DateTimeStyles.NoCurrentDateDefault,
                                       out result);

      return success;
    }
  }
}
