using System;
using System.Globalization;
using ADIF.NET.Helpers;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents an ADIF Location type.
  /// </summary>
  public class ADIFLocation : ADIFType<string> {

    /// <summary>
    /// 
    /// </summary>
    public override string Type => DataTypes.Location;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static Location Parse(string s)
    {
      if (string.IsNullOrEmpty(s))
        throw new Exception("Location value cannot be null or empty string.");

      s = s.Trim().ToUpper();

      if (s.Length != LocationHelper.LOCATION_LENGTH)
        throw new Exception("Invalid length.");

      var direction = s.Substring(0, 1);

      if (!LocationHelper.ValidateDirection(direction))
        throw new Exception($"Invalid direction indicator: '{direction}'");

      var degreesStr = s.Substring(1, 3);

      if (!int.TryParse(degreesStr, out int degrees))
        throw new Exception($"Degrees must be an integer.");

      if (!LocationHelper.ValidateDegrees(degrees))
        throw new Exception($"Invalid degrees value: '{degrees}'.");

      var minutesStr = s.Substring(5);

      if (!double.TryParse(minutesStr, out double minutes))
        throw new Exception($"Minutes must be a numeric value.");

      if (!LocationHelper.ValidateMinutes(minutes))
        throw new Exception($"Invalid minutes value: {minutes}");

      return new Location(direction, degrees, minutes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out Location result)
    {
      result = null;
      try
      {
        result = Parse(s);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class Location : IFormattable {

    /// <summary>
    /// 
    /// </summary>
    public string Direction { get; }

    /// <summary>
    /// 
    /// </summary>
    public int Degrees { get; }

    /// <summary>
    /// 
    /// </summary>
    public double Minutes { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    public Location(string location)
    {
      if (!ADIFLocation.TryParse(location, out Location result))
        throw new ArgumentException("Invalid location value.");

      Direction = result.Direction;
      Degrees = result.Degrees;
      Minutes = result.Minutes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="degrees"></param>
    /// <param name="minutes"></param>
    public Location(string direction,
                    int degrees,
                    double minutes)
    {
      if (!LocationHelper.ValidateDirection(direction))
        throw new ArgumentException("Invalid direction.");

      if (!LocationHelper.ValidateDegrees(degrees))
        throw new ArgumentException("Invalid degrees.");

      if (!LocationHelper.ValidateMinutes(minutes))
        throw new ArgumentException("Invalid minutes.");

      Direction = direction.ToUpper();
      Degrees = degrees;
      Minutes = minutes;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double ToDecimalDegrees()
    {
      // .d = M.m / 60;
      // Decimal Degrees = Degrees + .d
      var d = Minutes / 60;
      var temp = Degrees + d;
      return Direction == "S" || Direction == "W" ? Math.Abs(temp) * -1 : Math.Abs(temp);
    }

    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    public string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format)
      {
        case "G":
        case "L":
          return $"{ToString("D", provider)}{ToString("E", provider)} {ToString("M", provider)}";

        case "D":
          return Direction ?? string.Empty;

        case "E":
          return Degrees.ToString("000") ?? string.Empty;

        case "M":
          return Minutes.ToString("00.000") ?? string.Empty;

        default:
          throw new FormatException($"Format string '{format}' is not valid.");
      }
    }
  }
}

