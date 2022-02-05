using System;
using System.Collections.Generic;
using ADIF.NET.Tags;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper class for Location type.
  /// </summary>
  public static class LocationHelper {

    /// <summary>
    /// 
    /// </summary>
    public const int LOCATION_LENGTH = 11;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minutes"></param>
    public static bool ValidateMinutes(decimal minutes)
    {
      return minutes >= 00.000m && minutes <= 59.999m;
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
    public static bool ValidateDirection(string direction, bool latitude)
    {
      direction = (direction ?? string.Empty).ToUpper();

      if (latitude)
      {
        switch (direction)
        {
          case "N":
          case "S":
            return true;

          default:
            return false;
        }
      }
      else
      {
        switch (direction)
        {
          case "E":
          case "W":
            return true;

          default:
            return false;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public static bool ValidateDirection(string direction)
    {
      switch (direction)
      {
        case "N":
        case "S":
        case "E":
        case "W":
          return true;

        default:
          return false;
      }
    }
  }
}
