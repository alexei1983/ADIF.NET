﻿
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the Integer ADIF type.
    /// </summary>
    public class AdifInteger : AdifType<int?>
    {
        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => int.MinValue;

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
        public override string TypeName => DataTypeNames.Integer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override int? Parse(string? s)
        {
            try
            {
                if (!string.IsNullOrEmpty(s))
                    return int.Parse(s);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not convert value to {nameof(AdifInteger)}", nameof(s), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out int? result)
        {
            result = null;

            if (string.IsNullOrEmpty(s))
                return true;

            if (int.TryParse(s, out int intResult))
            {
                result = intResult;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValidValue(string? value)
        {
            if (string.IsNullOrEmpty(value) || int.TryParse(value, out int _))
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValidValue(object? value)
        {
            if (value is null)
                return true;

            if (value is int)
                return true;

            if (value is double dblVal)
                return dblVal.IsWholeNumber();

            return IsValidValue(value.ToString());
        }
    }
}
