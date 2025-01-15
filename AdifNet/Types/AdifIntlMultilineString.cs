
namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the IntlMultilineString ADIF type.
    /// </summary>
    public class AdifIntlMultilineString : AdifType<string>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.IntlMultilineString;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.IntlMultilineString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string Parse(string? s)
        {
            return s ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out string result)
        {
            result = s ?? string.Empty;
            return true;
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
            return true;
        }
    }
}
