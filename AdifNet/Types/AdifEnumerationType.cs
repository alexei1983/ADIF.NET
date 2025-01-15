
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the Enumeration ADIF type.
    /// </summary>
    public class AdifEnumerationType : AdifType<string>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.Enumeration;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.Enumeration;

        /// <summary>
        /// Whether or not the type is an enumeration.
        /// </summary>
        public override bool IsEnumeration => true;

        /// <summary>
        /// Validates the string representation of an ADIF Enumeration value. 
        /// </summary>
        /// <param name="s">String representation of an ADIF Enumeration value.</param>
        public override string Parse(string? s)
        {
            if (!new AdifString().TryParse(s, out string? result))
                throw new ArgumentException($"Invalid ADIF enumeration value: {s}", nameof(s));

            return result ?? string.Empty;
        }

        /// <summary>
        /// Validates the string representation of an ADIF Enumeration value. 
        /// </summary>
        /// <param name="s">String representation of an ADIF Enumeration value.</param>
        /// <param name="options">Valid enumeration options.</param>
        public string Parse(string? s, AdifEnumeration options)
        {
            var result = Parse(s);

            if (string.IsNullOrEmpty(s))
                return result;

            if (options != null)
            {
                if (!options.IsValid(result))
                    throw new ArgumentException($"Invalid ADIF Enumeration value: {s}", nameof(s));

                var option = options.GetValue(result);
                return option?.Code ?? throw new ArgumentException($"Invalid ADIF Enumeration value: {s}", nameof(s));
            }

            return result;
        }

        /// <summary>
        /// Validates the string representation of an ADIF Enumeration value. 
        /// A return value indicates whether the validation succeeded or failed.
        /// </summary>
        /// <param name="s">String representation of an ADIF Enumeration value.</param>
        /// <param name="result">Result of the conversion or validation operation.</param>
        public override bool TryParse(string? s, out string? result)
        {
            return new AdifString().TryParse(s, out result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">String representation of an ADIF Enumeration value.</param>
        /// <param name="options">Valid enumeration options.</param>
        /// <param name="result">Result of the conversion or validation operation.</param>
        public bool TryParse(string? s, AdifEnumeration options, out string? result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = null;
                return true;
            }

            if (TryParse(s, out result))
            {
                if (options != null)
                {
                    if (!options.IsValid(result))
                    {
                        result = null;
                        return false;
                    }

                    var option = options.GetValue(result);
                    result = option?.Code;
                }

                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        public override bool IsValidValue(object? o)
        {
            return IsValidValue(o == null ? string.Empty : o.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override bool IsValidValue(string? s)
        {
            return TryParse(s, out _);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="options"></param>
        public bool IsValidValue(object? o, AdifEnumeration options)
        {
            return IsValidValue(o == null ? string.Empty : o.ToString(), options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="options"></param>
        public bool IsValidValue(string? s, AdifEnumeration options)
        {
            return TryParse(s, options, out _);
        }
    }
}
