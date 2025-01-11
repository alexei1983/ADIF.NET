
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Details of the logging station's Morse key (e.g. make, model, etc).
    /// </summary>
    public class MyMorseKeyInfoTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyMorseKeyInfo;

        /// <summary>
        /// Creates a new MY_MORSE_KEY_INFO tag.
        /// </summary>
        public MyMorseKeyInfoTag() { }

        /// <summary>
        /// Creates a new MY_MORSE_KEY_INFO tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyMorseKeyInfoTag(string value) : base(value) { }
    }
}
