
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's Morse key type (e.g. straight key, bug, etc).
    /// </summary>
    public class MyMorseKeyTypeTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyMorseKeyType;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.MorseKeyTypes;

        /// <summary>
        /// Creates a new MY_MORSE_KEY_TYPE tag.
        /// </summary>
        public MyMorseKeyTypeTag() { }

        /// <summary>
        /// Creates a new MY_MORSE_KEY_TYPE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyMorseKeyTypeTag(string value) : base(value) { }
    }
}
