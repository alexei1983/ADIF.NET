
using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value is either free-form text or selected 
    /// from a list of options.
    /// </summary>
    public class EnumerationTag : Tag<string>, ITag
    {
        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifEnumerationType();

        /// <summary>
        /// Creates a new instance of the <see cref="EnumerationTag"/> class.
        /// </summary>
        public EnumerationTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="EnumerationTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public EnumerationTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new instance of the <see cref="EnumerationTag"/> class.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public EnumerationTag(AdifEnumerationValue enumValue) : base(enumValue.Code) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual bool IsValidOption(string option)
        {
            if (Options.Any())
                return Options.IsValid(option) || !RestrictOptions;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object? ConvertValue(object? value)
        {
            if (value == null)
                return value;

            string _value;
            if (value is string strVal)
                _value = strVal;
            else if (value is AdifEnumerationValue enumVal)
                _value = enumVal.Code;
            else
                _value = value?.ToString() ?? string.Empty;

            if (!IsValidOption(_value) && RestrictOptions)
                throw new InvalidEnumerationOptionException($"Invalid enumeration value for tag {Name}: {_value}");

            return _value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
