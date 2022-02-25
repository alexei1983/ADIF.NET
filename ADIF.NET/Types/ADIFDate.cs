using System;
using System.Globalization;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Date ADIF type.
  /// </summary>
  public class ADIFDate : ADIFType<DateTime> {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Date;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static DateTime Parse(string s)
    {
      if (!FromString(s, out DateTime result))
        throw new ArgumentException($"Invalid ADIF Date: '{s ?? string.Empty}'");

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

      success = DateTime.TryParseExact(s ?? string.Empty,
                                       Values.ADIF_DATE_FORMAT, 
                                       CultureInfo.CurrentCulture, 
                                       DateTimeStyles.NoCurrentDateDefault,
                                       out result);

      return success;
    }
  }
}
