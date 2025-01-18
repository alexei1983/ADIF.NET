
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's Morse key type (e.g. straight key, bug, etc).
    /// </summary>
    public class MorseKeyTypeTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MorseKeyType;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.MorseKeyTypes;

        /// <summary>
        /// Creates a new MORSE_KEY_TYPE tag.
        /// </summary>
        public MorseKeyTypeTag() { }

        /// <summary>
        /// Creates a new MORSE_KEY_TYPE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MorseKeyTypeTag(string value) : base(value) { }
    }
}
