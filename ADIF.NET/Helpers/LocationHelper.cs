using System;
using System.Collections.Generic;
using ADIF.NET.Tags;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper class for location.
  /// </summary>
  public static class LocationHelper {

    public const int LOCATION_LENGTH = 11;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minutes"></param>
    public static bool ValidateMinutes(double minutes)
    {
      return minutes >= 00.000d && minutes <= 59.999d;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="degrees"></param>
    public static bool ValidateDegrees(int degrees)
    {
      return degrees >= 0 && degrees <= 180;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public static bool ValidateDirection(string direction)
    {
      direction = (direction ?? string.Empty).ToUpper();

      switch (direction)
      {
        case "E":
        case "W":
        case "N":
        case "S":
          return true;

        default:
          return false;
      }
    }
  }
}
