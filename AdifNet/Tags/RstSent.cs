
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the signal report sent to the contacted station.
    /// </summary>
    public class RstSentTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.RstSent;

        /// <summary>
        /// Creates a new RST_SENT tag.
        /// </summary>
        public RstSentTag() { }

        /// <summary>
        /// Creates a new RST_SENT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RstSentTag(string value) : base(value) { }
    }
}
