using System;

namespace org.goodspace.Data.Radio.Adif.Types
{

    /// <summary>
    /// Represents the PositiveInteger ADIF type.
    /// </summary>
    public class AdifPositiveInteger : AdifType<int?>
    {

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => 1;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override double MaxValue => int.MaxValue;

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Number;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.PositiveInteger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public static int? Parse(string? s)
        {
            try
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var intResult = int.Parse(s);
                    if (intResult < 1)
                        throw new ArgumentException("Value must be greater than zero.", nameof(s));

                    return intResult;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not convert value to {nameof(AdifPositiveInteger)}", nameof(s), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public static bool TryParse(string? s, out int? result)
        {
            result = null;

            if (string.IsNullOrEmpty(s))
                return true;

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
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsValidValue(string? value)
        {
            return string.IsNullOrEmpty(value) || (int.TryParse(value, out int intVal) && intVal > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsValidValue(object? value)
        {
            if (value is null)
                return true;

            if (value.IsWholeNumber() && (long)value > 0)
                return true;

            return IsValidValue(value == null ? string.Empty : value.ToString());
        }
    }
}
