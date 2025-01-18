using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Types
{
    /// <summary>
    /// Represents the SponsoredAwardList ADIF type.
    /// </summary>
    public class AdifSponsoredAwardList : AdifType<string[]>, IAdifType
    {
        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public override string Type => DataTypes.SponsoredAwardList;

        /// <summary>
        /// ADIF data type name.
        /// </summary>
        public override string TypeName => DataTypeNames.SponsoredAwardList;

        /// <summary>
        /// 
        /// </summary>
        public override bool MultiValue => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override string[] Parse(string? s)
        {
            return ParseAwardList(s, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        public override bool TryParse(string? s, out string[] result)
        {
            try
            {
                result = ParseAwardList(s, true);
                return true;
            }
            catch
            {
                result = [];
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

            return IsValidValue(o.ToString());
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
                ParseAwardList(s, true);
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
            var result = TryParse(s, out string[]? _value);
            value = _value;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="throwExceptions"></param>
        static string[] ParseAwardList(string? s, bool throwExceptions)
        {
            if (string.IsNullOrEmpty(s))
                return [];

            var prefixes = AdifEnumerations.SponsoredAwardPrefixes.GetValues();
            var result = new List<string>();
            var exceptions = new List<Exception>();

            // split by comma
            var split = s.Split(AdifConstants.Comma);

            foreach (var award in split)
            {
                var checkedCount = 0;

                foreach (var prefix in prefixes)
                {
                    if (!award.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        checkedCount++;
                }

                if (checkedCount >= prefixes.Length)
                    exceptions.Add(new SponsoredAwardListException($"Award '{award.ToUpper()}' does not have a valid sponsored prefix.", award.ToUpper()));
                else
                    result.Add(award);
            }

            if (throwExceptions)
            {
                if (exceptions.Count > 1)
                    throw new AggregateException([..exceptions]);
                else if (exceptions.Count == 1)
                    throw exceptions[0];
            }

            return [.. result];
        }
    }
}
