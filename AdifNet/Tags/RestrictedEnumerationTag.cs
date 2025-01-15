
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents an ADIF.NET tag where the underlying value must be selected from a list 
    /// of valid options.
    /// </summary>
    public class RestrictedEnumerationTag : EnumerationTag, ITag
    {
        /// <summary>
        /// Whether or not to restrict the tag value to the specified enumeration options.
        /// </summary>
        public override bool RestrictOptions => true;

        /// <summary>
        /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
        /// </summary>
        public RestrictedEnumerationTag() { }

        /// <summary>
        /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RestrictedEnumerationTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public RestrictedEnumerationTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
