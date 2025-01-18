using System.Globalization;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the Date ADIF type.
    /// </summary>
    public class AdifDate : AdifType<DateTime>
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Date;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.Date;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override DateTime Parse(string? s)
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
            return FromString(value ?? string.Empty, out DateTime _);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        static bool FromString(string? s, out DateTime result)
        {
            bool success = DateTime.TryParseExact(s,
                                                 AdifConstants.DateFormat,
                                                 CultureInfo.CurrentCulture,
                                                 DateTimeStyles.NoCurrentDateDefault,
                                                 out result);

            return success;
        }
    }
}
