using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the SecondaryAdministrativeSubdivisionListAlt ADIF type.
    /// </summary>
    public class AdifSecondarySubdivisionListAlt : AdifType<string[]>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => string.Empty;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.SecondaryAdministrativeSubdivisionListAlt;

        /// <summary>
        /// 
        /// </summary>
        public override bool MultiValue => true;

        /// <summary>
        /// 
        /// </summary>
        public override bool IsEnumeration => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string[] Parse(string? s)
        {
            return ParseList(s, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out string[]? result)
        {
            try
            {
                result = ParseList(s, true);
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
            if (o is null)
                return true;

            string? strVal;
            var objType = o.GetType();
            if (o is string _strVal)
                strVal = _strVal;
            else if (o is AdifEnumerationValue enumVal)
                strVal = enumVal.Code;
            else if (objType.IsAssignableFrom(typeof(IEnumerable<AdifEnumerationValue>)))
                strVal = string.Join(AdifConstants.Semicolon, ((IEnumerable<AdifEnumerationValue>)o).Select(e => e.Code));
            else if (objType.IsAssignableFrom(typeof(IEnumerable<string>)))
                strVal = string.Join(AdifConstants.Semicolon, (IEnumerable<string>)o);
            else
                strVal = o.ToString();

            return IsValidValue(strVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override bool IsValidValue(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            try
            {
                ParseList(s, true);
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
        /// <param name="s"></param>
        /// <param name="throwExceptions"></param>
        string[] ParseList(string? s, bool throwExceptions)
        {
            if (string.IsNullOrEmpty(s))
                return [];

            List<string> returnVal = [];

            if (s.Contains(AdifConstants.Semicolon))
            {
                var values = s.Split(AdifConstants.Semicolon, StringSplitOptions.RemoveEmptyEntries |
                                                       StringSplitOptions.TrimEntries);

                if (values?.Length > 0)
                {
                    foreach (var val in values)
                    {
                        if (!AdifEnumerations.SecondarySubdivisionAlts.IsValid(val) && throwExceptions)
                            throw new InvalidEnumerationOptionException($"Invalid value for ADIF type {TypeName}: {val}");
                        else
                            returnVal = [.. returnVal, val];
                    }
                }
            }
            return [.. returnVal];
        }
    }
}
