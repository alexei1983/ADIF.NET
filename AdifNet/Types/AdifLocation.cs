using System.Globalization;
using org.goodspace.Data.Radio.Adif.Helpers;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Location type (e.g. latitude or longitude).
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// Unspecified location type.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Latitude.
        /// </summary>
        Latitude,

        /// <summary>
        /// Longitude.
        /// </summary>
        Longitude,
    }

    /// <summary>
    /// Represents the Location ADIF type.
    /// </summary>
    public class AdifLocation : AdifType<Location>, IAdifType
    {
        /// <summary>
        /// The ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Location;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.Location;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override Location Parse(string? s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("Invalid ADIF location.", nameof(s));

            s = s.Trim().ToUpper();

            if (s.Length != LocationHelper.LOCATION_LENGTH)
                throw new Exception("Invalid length.");

            var direction = s[..1];

            if (!LocationHelper.ValidateDirection(direction))
                throw new Exception($"Invalid direction indicator: '{direction}'");

            var degreesStr = s.Substring(1, 3);

            if (!int.TryParse(degreesStr, out int degrees))
                throw new Exception($"Degrees must be an integer.");

            if (!LocationHelper.ValidateDegrees(degrees))
                throw new Exception($"Invalid degrees value: '{degrees}'.");

            var minutesStr = s[5..];

            if (!decimal.TryParse(minutesStr, out decimal minutes))
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
        public override bool TryParse(string? s, out Location? result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Determines whether or not the specified string is a valid ADIF Location value.
        /// </summary>
        /// <param name="value">String to check for validity.</param>
        public override bool IsValidValue(string? value)
        {
            return string.IsNullOrEmpty(value) || TryParse(value, out Location? _);
        }

        /// <summary>
        /// Determines whether or not the specified object is a valid ADIF Location value.
        /// </summary>
        /// <param name="value">Value to check for validity.</param>
        public override bool IsValidValue(object? value)
        {
            if (value is Location)
                return true;

            return IsValidValue(value is null ? string.Empty : value.ToString());
        }

        /// <summary>
        /// Creates new <see cref="Location"/> instances from the specified latitude and longitude 
        /// decimal degree values.
        /// </summary>
        /// <param name="latitude">Decimal degree value representing latitude.</param>
        /// <param name="longitude">Decimal degree value representing longitude.</param>
        /// <param name="latitudeLocation"><see cref="Location"/> instance created from the latitude decimal degrees.</param>
        /// <param name="longitudeLocation"><see cref="Location"/> instance created from the longitude decimal degrees.</param>
        public static void FromDecimalDegrees(decimal latitude,
                                              decimal longitude,
                                              out Location latitudeLocation,
                                              out Location longitudeLocation)
        {
            latitudeLocation = FromDecimalDegrees(latitude, LocationType.Latitude);
            longitudeLocation = FromDecimalDegrees(longitude, LocationType.Longitude);
        }

        /// <summary>
        /// Creates a new <see cref="Location"/> instance from the specified decimal degrees.
        /// </summary>
        /// <param name="decimalDegrees">Decimal degrees representing the location.</param>
        /// <param name="type">The type of the location (e.g. whether latitude or longitude).</param>
        public static Location FromDecimalDegrees(decimal decimalDegrees, LocationType type)
        {
            if (type == LocationType.Latitude && (decimalDegrees < -90 || decimalDegrees > 90))
                throw new ArgumentException("Invalid latitude decimal degrees.", nameof(decimalDegrees));
            else if (type == LocationType.Longitude && (decimalDegrees < -180 || decimalDegrees > 180))
                throw new ArgumentException("Invalid longitude decimal degrees.", nameof(decimalDegrees));
            else if (type == LocationType.Unspecified)
                throw new ArgumentException("Type must be latitude or longitude.", nameof(type));

            var degrees = Math.Floor(Math.Truncate(100 * Math.Abs(decimalDegrees)) / 100);
            var decimalPart = (Math.Abs(decimalDegrees) - degrees) * 60;

            var direction = string.Empty;
            if (type == LocationType.Latitude)
            {
                if (decimalDegrees >= 0)
                    direction = "N";
                else if (decimalDegrees < 0)
                    direction = "S";
            }
            else if (type == LocationType.Longitude)
            {
                if (decimalDegrees >= 0)
                    direction = "E";
                else if (decimalDegrees < 0)
                    direction = "W";
            }

            return new Location(direction, (int)degrees, decimalPart);
        }
    }

    /// <summary>
    /// Represents the value of an ADIF Location type.
    /// </summary>
    public class Location : IFormattable
    {
        /// <summary>
        /// The type of the location (e.g. whether latitude or longitude).
        /// </summary>
        public LocationType LocationType { get; private set; }

        /// <summary>
        /// Directional indicator for the location.
        /// </summary>
        public string Direction { get; }

        /// <summary>
        /// Degrees for the location.
        /// </summary>
        public int Degrees { get; }

        /// <summary>
        /// Minutes for the location.
        /// </summary>
        public decimal Minutes { get; }

        /// <summary>
        /// Sets the appropriate location type.
        /// </summary>
        void SetLocationType()
        {
            if (!string.IsNullOrEmpty(Direction))
                LocationType = Direction == "N" || Direction == "S" ? LocationType.Latitude :
                          Direction == "E" || Direction == "W" ? LocationType.Longitude :
                          LocationType.Unspecified;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="location">ADIF string location.</param>
        public Location(string location)
        {
            if (!new AdifLocation().TryParse(location, out Location? result))
                throw new ArgumentException("Invalid location value.", nameof(location));

            if (result == null)
                throw new ArgumentException("Invalid location value.", nameof(location));


            Direction = result.Direction;
            Degrees = result.Degrees;
            Minutes = result.Minutes;

            SetLocationType();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="direction">Directional indicator.</param>
        /// <param name="degrees">Degrees.</param>
        /// <param name="minutes">Minutes.</param>
        public Location(string direction,
                        int degrees,
                        decimal minutes)
        {
            if (!LocationHelper.ValidateDegrees(degrees))
                throw new ArgumentException("Invalid degrees.");

            if (!LocationHelper.ValidateMinutes(minutes))
                throw new ArgumentException("Invalid minutes.");

            if (!LocationHelper.ValidateDirection(direction))
                throw new ArgumentException("Invalid direction.");

            Direction = direction.ToUpper();
            Degrees = degrees;
            Minutes = minutes;

            SetLocationType();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="decimalDegrees">Decimal degree value representing the location.</param>
        /// <param name="type">The location type (e.g. whether latitude or longitude).</param>
        public Location(decimal decimalDegrees, LocationType type)
        {
            var location = AdifLocation.FromDecimalDegrees(decimalDegrees, type);
            Direction = location.Direction;
            Degrees = location.Degrees;
            Minutes = location.Minutes;
            LocationType = location.LocationType;
        }

        /// <summary>
        /// Creates a new <see cref="Location"/> instance from the specified decimal degrees.
        /// </summary>
        /// <param name="decimalDegrees">Decimal degrees representing the location.</param>
        /// <param name="type">The type of the location (e.g. whether latitude or longitude).</param>
        public static Location FromDecimalDegrees(decimal decimalDegrees, LocationType type)
        {
            return AdifLocation.FromDecimalDegrees(decimalDegrees, type);
        }

        /// <summary>
        /// Converts the current <see cref="Location"/> instance to decimal degrees.
        /// </summary>
        public decimal ToDecimalDegrees()
        {
            // .d = M.m / 60;
            // Decimal Degrees = Degrees + .d
            var d = Minutes / 60;
            var temp = Degrees + d;
            return Direction == "S" || Direction == "W" ? Math.Abs(temp) * -1 : Math.Abs(temp);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="Location"/> instance.
        /// </summary>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="Location"/> instance.
        /// </summary>
        /// <param name="format">Format string.</param>
        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="Location"/> instance.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="provider">Culture-specific format provider.</param>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            provider ??= CultureInfo.CurrentCulture;

            return format switch
            {
                "G" or "L" => $"{ToString("C", provider)}{ToString("D", provider)} {ToString("M", provider)}",
                "C" => Direction ?? string.Empty,
                "D" => Degrees.ToString("000"),
                "M" => Minutes.ToString("00.000"),
                "DD" => ToDecimalDegrees().ToString(provider),
                _ => throw new FormatException($"Format string '{format}' is not valid."),
            };
        }
    }
}

