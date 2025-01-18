using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is selected from a list but the field 
    /// can contain more than 1 value.
    /// </summary>
    public class MultiValueEnumerationTag : EnumerationTag, ITag
    {
        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifEnumerationType();

        /// <summary>
        /// Value separator.
        /// </summary>
        public override string ValueSeparator => AdifConstants.Semicolon.ToString();

        /// <summary>
        /// Creates a new instance of the <see cref="MultiValueEnumerationTag"/> class.
        /// </summary>
        public MultiValueEnumerationTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="MultiValueEnumerationTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MultiValueEnumerationTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MultiValueEnumerationTag"/> class.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public MultiValueEnumerationTag(AdifEnumerationValue enumValue) : base(enumValue) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object? ConvertValue(object? value)
        {
            if (value == null)
                return value;

            List<string> returnVal = [];

            string _value;
            if (value is AdifEnumerationValue enumVal)
                return base.ConvertValue(enumVal);
            else if (typeof(IEnumerable<AdifEnumerationValue>).IsAssignableFrom(value.GetType()))
            {
                foreach (var val in (IEnumerable<AdifEnumerationValue>)value)
                {
                    if (!IsValidOption(val.Code))
                        throw new InvalidEnumerationOptionException($"Invalid enumeration value for tag {Name}: {val.Code}");
                    returnVal = [.. returnVal, val.Code];
                }
                return returnVal.ToArray();
            }
            else if (typeof(IEnumerable<string>).IsAssignableFrom(value.GetType()))
            {
                foreach (var val in (IEnumerable<string>)value)
                {
                    if (!IsValidOption(val))
                        throw new InvalidEnumerationOptionException($"Invalid enumeration value for tag {Name}: {val}");
                    returnVal = [.. returnVal, val];
                }
                return returnVal.ToArray();
            }
            else if (value is string strVal)
                _value = strVal;
            else
                _value = value.ToString() ?? string.Empty;

            return AdifType.Parse(_value);
        }
    }
}