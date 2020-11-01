using System;
using System.Globalization;

namespace ADIF.NET {

  public class AdifLocation : IFormattable {

    public int ExpectedLength => LENGTH;
    
    public int Length => ToString().Length;

    public string Direction { get; set; }

    public int Degrees { get; set; }

    public double Minutes { get; set; }

    public AdifLocation(string location) {

      if (!TryParse(location, out AdifLocation adifLocation))
        throw new ArgumentException("Invalid location value.");

      Direction = adifLocation.Direction;
      Degrees = adifLocation.Degrees;
      Minutes = adifLocation.Minutes;
      }

    public AdifLocation(string direction, 
                        int degrees,
                        double minutes) {

      if (!ValidateDirection(direction))
        throw new ArgumentException("Invalid direction.");

      if (!ValidateDegrees(degrees))
        throw new ArgumentException("Invalid degrees.");

      if (!ValidateMinutes(minutes))
        throw new ArgumentException("Invalid minutes.");

      Direction = direction.ToUpper();
      Degrees = degrees;
      Minutes = minutes;
      }

    public AdifLocation() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public static bool TryParse(string value, out AdifLocation location) {

      location = default(AdifLocation);

      value = (value ?? string.Empty).Trim().ToUpper();

      if (value.Length != LENGTH)
        return false;

      if (value.Substring(4, 1) != " ")
        return false;

      var direction = value.Substring(0, 1);

      if (!ValidateDirection(direction))
        return false;

      var degreesStr = value.Substring(1, 3);
      
      if (!int.TryParse(degreesStr, out int degrees))
        return false;

      if (!ValidateDegrees(degrees))
        return false;

      var minutesStr = value.Substring(5);

      if (!double.TryParse(minutesStr, out double minutes))
        return false;

      if (!ValidateMinutes(minutes))
        return false;

      location = new AdifLocation(direction, degrees, minutes);
      return true;
      }

    public double ToDecimalDegrees() {
      // Decimal Degrees = degrees + (minutes / 60) + (seconds / 3600)

      var minutesParts = Minutes.SplitDouble();

      return (Degrees + (minutesParts.Item1 / 60) + (minutesParts.Item2 / 3600));
      }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
      return ToString("G", CultureInfo.CurrentCulture);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToString(string format) {
      return ToString(format, CultureInfo.CurrentCulture);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public string ToString(string format, IFormatProvider provider) {

      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format) {

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minutes"></param>
    /// <returns></returns>
    static bool ValidateMinutes(double minutes) {
      return minutes >= 00.000d && minutes <= 59.999d;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    static bool ValidateDegrees(int degrees) {
      return degrees >= 0 && degrees <= 180;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    static bool ValidateDirection(string direction) {
      direction = (direction ?? string.Empty).ToUpper();

      switch (direction) {
      case "E":
      case "W":
      case "N":
      case "S":
        return true;

      default:
        return false;
        }
      }

    const int LENGTH = 11;
    }
  }
