
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL card message.
    /// </summary>
    public class QslMsgRcvdTag : MultilineStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslMsgRcvd;

        /// <summary>
        /// Creates a new QSLMSG_RCVD tag.
        /// </summary>
        public QslMsgRcvdTag() { }

        /// <summary>
        /// Creates a new QSLMSG_RCVD tag.
        /// </summary>
        /// <param name="value">QSL card message.</param>
        public QslMsgRcvdTag(string value) : base(value) { }
    }
}
