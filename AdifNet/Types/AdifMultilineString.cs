
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the MultilineString ADIF type.
    /// </summary>
    public class AdifMultilineString : AdifType<string>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.MultilineString;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.MultilineString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string Parse(string? s)
        {
            s ??= string.Empty;

            if (!s.IsAscii())
                throw new ArgumentException("Invalid ADIF MultilineString.", nameof(s));

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out string? result)
        {
            result = null;
            try
            {
                result = Parse(s);
                return true;
            }
            catch
            {
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
            return s.IsAscii();
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
