
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the signal report from the contacted station.
    /// </summary>
    public class RstRcvdTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.RstRcvd;

        /// <summary>
        /// Creates a new RST_RCVD tag.
        /// </summary>
        public RstRcvdTag() { }

        /// <summary>
        /// Creates a new RST_RCVD tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RstRcvdTag(string value) : base(value) { }
    }
}
