﻿using System.Globalization;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the Time ADIF type.
    /// </summary>
    public class AdifTime : AdifType<DateTime>
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Time;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.Time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override DateTime Parse(string? s)
        {
            if (!FromString(s, out DateTime result))
                throw new ArgumentException($"Invalid string value: '{s ?? string.Empty}'");

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out DateTime result)
        {
            return FromString(s, out result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValidValue(object? value)
        {
            if (value is DateTime || value is DateTime?)
                return true;

            return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValidValue(string? value)
        {
            return FromString(value == null ? string.Empty : value.ToString(), out DateTime _);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        static bool FromString(string? s, out DateTime result)
        {
            s ??= string.Empty;

            bool success = DateTime.TryParseExact(s,
                                                  s.Length > 4 ? AdifConstants.TimeFormatLong : AdifConstants.TimeFormatShort,
                                                  CultureInfo.CurrentCulture,
                                                  DateTimeStyles.NoCurrentDateDefault,
                                                  out result);

            return success;
        }
    }
}
