
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Details of the contacted station's Morse key (e.g. make, model, etc).
    /// </summary>
    public class MorseKeyInfoTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MorseKeyInfo;

        /// <summary>
        /// Creates a new MORSE_KEY_INFO tag.
        /// </summary>
        public MorseKeyInfoTag() { }

        /// <summary>
        /// Creates a new MORSE_KEY_INFO tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MorseKeyInfoTag(string value) : base(value) { }
    }
}
