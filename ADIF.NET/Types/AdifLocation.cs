using System;
using System.Globalization;

namespace ADIF.NET.Types {

  public class ADIFLocation : ADIFType<string>, IFormattable {

    public override string Type => DataTypes.Location;

    public string Direction { get; }

    public int Degrees { get; }

    public double Minutes { get; }

    public ADIFLocation(string location) {

      if (!TryParse(location, out ADIFLocation adifLocation))
        throw new ArgumentException("Invalid location value.");

      Direction = adifLocation.Direction;
      Degrees = adifLocation.Degrees;
      Minutes = adifLocation.Minutes;
      }

    public ADIFLocation(string direction, 
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

    public ADIFLocation() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParse(string s, out ADIFLocation result) {

      result = default(ADIFLocation);

      s = (s ?? string.Empty).Trim().ToUpper();

      if (s.Length != LENGTH)
        return false;

      if (s.Substring(4, 1) != " ")
        return false;

      var direction = s.Substring(0, 1);

      if (!ValidateDirection(direction))
        return false;

      var degreesStr = s.Substring(1, 3);
      
      if (!int.TryParse(degreesStr, out int degrees))
        return false;

      if (!ValidateDegrees(degrees))
        return false;

      var minutesStr = s.Substring(5);

      if (!double.TryParse(minutesStr, out double minutes))
        return false;

      if (!ValidateMinutes(minutes))
        return false;

      result = new ADIFLocation(direction, degrees, minutes);
      return true;
      }

    public double ToDecimalDegrees() {
      // Decimal Degrees = degrees + (minutes / 60) + (seconds / 3600)

      Minutes.SplitDouble(out double mins, out double secs);

      var degrees = Direction == "S" || Direction == "W" ? Math.Abs(Degrees) * -1 : Math.Abs(Degrees);

      return degrees + ((mins / 60) + (secs / 3600));
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
