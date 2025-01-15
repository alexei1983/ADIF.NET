
using org.goodspace.Data.Radio.Adif.Types;
using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/> with 
    /// optional Unicode and line endings.
    /// </summary>
    public class IntlMultilineStringTag : Tag<string>, ITag
    {
        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifIntlMultilineString();

        /// <summary>
        /// Converts the specified object to an ADIF IntlMultilineString.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        public override object? ConvertValue(object? value)
        {
            try
            {
                var result = AdifType.Parse(value == null ? string.Empty : value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw new ValueConversionException(value, Name, ex);
            }
        }

        /// <summary>
        /// Determines whether or not the specified object can be represented as an ADIF IntlMultilineString.
        /// </summary>
        /// <param name="value">Value to validate.</param>
        public override bool ValidateValue(object? value)
        {
            try
            {
                ConvertValue(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="IntlMultilineStringTag"/> class.
        /// </summary>
        public IntlMultilineStringTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="IntlMultilineStringTag"/> class.
        /// </summary>
        /// <param name="value">Initial value for the tag.</param>
        public IntlMultilineStringTag(string value) : base(value) { }

    }
}
