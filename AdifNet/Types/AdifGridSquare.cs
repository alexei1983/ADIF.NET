using org.goodspace.Data.Radio.Adif.Exceptions;
using Unclassified.Util;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the GridSquare ADIF type.
    /// </summary>
    public class AdifGridSquare : AdifType<string>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => string.Empty;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.GridSquare;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string Parse(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            var len = s.Length;

            if (len != 2 && len != 4 && len != 6 && len != 8)
                throw new GridSquareException($"Invalid GridSquare length: {len}", s);

            try
            {
                MaidenheadLocator.LocatorToLatLng(s);
            }
            catch (Exception ex)
            {
                throw new GridSquareException($"Invalid GridSquare: {s}", s, ex);
            }

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
    }
}
