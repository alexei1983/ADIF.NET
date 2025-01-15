
using org.goodspace.Data.Radio.Adif.Types;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF tag of type SponsoredAwardList.
    /// </summary>
    public class SponsoredAwardListTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// String that delimits values in a multivalued ADIF tag.
        /// </summary>
        public override string ValueSeparator => Values.COMMA.ToString();

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifSponsoredAwardList();

        /// <summary>
        /// Valid sponsored award prefixes.
        /// </summary>
        public string[] Prefixes => Values.SponsoredAwardPrefixes.GetValues();

        /// <summary>
        /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
        /// </summary>
        public SponsoredAwardListTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
        /// </summary>
        /// <param name="value"></param>
        public SponsoredAwardListTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new instance of the <see cref="SponsoredAwardListTag"/>.
        /// </summary>
        /// <param name="values"></param>
        public SponsoredAwardListTag(params string[] values) : base(values) { }

        /// <summary>
        /// Determines whether or not the specified object is a valid value for the current ADIF tag.
        /// </summary>
        /// <param name="value">Object to validate.</param>
        public override bool ValidateValue(object? value)
        {
            if (value != null)
            {
                try
                {
                    AdifType.Parse(value.ToString());
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Converts the specified object to the appropriate type for the current ADIF tag.
        /// </summary>
        /// <param name="value">Object to convert.</param>
        public override object? ConvertValue(object? value)
        {
            string? strVal;
            if (value is string strValue)
                strVal = strValue;
            else
                strVal = value != null ? value.ToString() : string.Empty;

            try
            {
                return AdifType.Parse(strVal);
            }
            catch (Exception ex)
            {
                throw new ValueConversionException(value, Name, ex);
            }
        }

        /// <summary>
        /// Adds a new sponsored award value to the tag.
        /// </summary>
        /// <param name="award">Sponsored award to add.</param>
        public override void AddValue(string award)
        {
            if (string.IsNullOrEmpty(award))
                throw new ArgumentException("Award is required.", nameof(award));

            if (!AdifType.TryParse(award, out _))
                throw new SponsoredAwardListException($"Award '{award}' does not have a valid sponsored prefix.", award);

            base.AddValue(award);
        }
    }
}
