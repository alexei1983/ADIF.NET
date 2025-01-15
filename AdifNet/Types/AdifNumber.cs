
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the Number ADIF type.
    /// </summary>
    public class AdifNumber : AdifType<double?>
    {
        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => double.MinValue;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override double MaxValue => double.MaxValue;

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Number;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.Number;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override double? Parse(string? s)
        {
            try
            {
                if (!string.IsNullOrEmpty(s))
                    return double.Parse(s);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not convert value to {nameof(AdifNumber)}", nameof(s), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out double? result)
        {
            result = null;

            if (string.IsNullOrEmpty(s))
                return true;

            if (double.TryParse(s, out double dblResult))
            {
                result = dblResult;
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
            if (string.IsNullOrEmpty(value) || double.TryParse(value, out double _))
                return true;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool IsValidValue(object? value)
        {
            if (value is double || value is double?)
                return true;

            return IsValidValue(value == null ? string.Empty : value.ToString());
        }
    }
}
