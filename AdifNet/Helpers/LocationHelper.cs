
namespace org.goodspace.Data.Radio.Adif.Helpers
{
    /// <summary>
    /// Helper class for the ADIF Location type.
    /// </summary>
    internal static class LocationHelper
    {
        const string NORTH = "N";
        const string SOUTH = "S";
        const string EAST = "E";
        const string WEST = "W";

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
        /// <param name="latitude"></param>
        public static bool ValidateDirection(string direction, bool latitude)
        {
            direction = (direction ?? string.Empty).ToUpper();

            if (latitude)
            {
                return direction switch
                {
                    NORTH or SOUTH => true,
                    _ => false,
                };
            }
            else
            {
                return direction switch
                {
                    EAST or WEST => true,
                    _ => false,
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public static bool ValidateDirection(string direction)
        {
            direction = (direction ?? string.Empty).ToUpper();

            return direction switch
            {
                NORTH or SOUTH or EAST or WEST => true,
                _ => false,
            };
        }
    }
}
