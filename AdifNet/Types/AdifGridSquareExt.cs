using org.goodspace.Data.Radio.Adif.Exceptions;
using System.Text.RegularExpressions;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the GridSquareExt ADIF type.
    /// </summary>
    public partial class AdifGridSquareExt : AdifType<string>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => string.Empty;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.GridSquareExt;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string Parse(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            var len = s.Length;

            if (len != 2 && len != 4)
                throw new GridSquareException($"Invalid GridSquareExt length: {len}", s);

            if (!GridSquareExtRegex().IsMatch(s))
                throw new GridSquareException($"Invalid GridSquareExt: {s}", s);

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
            return IsValidValue(o is null ? string.Empty : o.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override bool IsValidValue(string? s)
        {
            try
            {
                Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        const string GRIDSQUARE_EXT_REGEX = "^[A-R]{2}([0-9]{2})?$";

        [GeneratedRegex(GRIDSQUARE_EXT_REGEX, RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex GridSquareExtRegex();
    }
}
