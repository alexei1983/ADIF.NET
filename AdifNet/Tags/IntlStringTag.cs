using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/> 
    /// with the potential presence of non-ASCII characters.
    /// </summary>
    public class IntlStringTag : Tag<string>, ITag
    {
        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifIntlString();

        /// <summary>
        /// Creates a new instance of the <see cref="IntlStringTag"/> class.
        /// </summary>
        public IntlStringTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="IntlStringTag"/> class.
        /// </summary>
        /// <param name="value">Initial value for the tag.</param>
        public IntlStringTag(string value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override object? ConvertValue(object? value)
        {
            try
            {
                var strVal = value is string strValue ? strValue : value != null ? value.ToString() : string.Empty;
                return AdifType.Parse(strVal);
            }
            catch (Exception ex)
            {
                throw new ValueConversionException(value, Name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
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
    }
}
