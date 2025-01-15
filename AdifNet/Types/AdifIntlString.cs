
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the IntlString ADIF type.
    /// </summary>
    public class AdifIntlString : AdifType<string>, IAdifType
    {
        /// <summary>
        /// The ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.IntlString;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.IntlString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string Parse(string? s)
        {
            s ??= string.Empty;

            if (s.HasLineEnding())
                throw new ArgumentException("Invalid ADIF IntlString: value contains line endings.", nameof(s));

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out string? result)
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
            s ??= string.Empty;
            return !s.HasLineEnding();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        object IAdifType.Parse(string? s)
        {
            return Parse(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IAdifType.TryParse(string? s, out object? value)
        {
            var result = TryParse(s, out string? _value);
            value = _value;
            return result;
        }
    }
}
