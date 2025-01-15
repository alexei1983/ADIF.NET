
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL card message.
    /// </summary>
    public class QslMsgIntlTag : IntlMultilineStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslMsgIntl;

        /// <summary>
        /// Creates a new QSLMSG_INTL tag.
        /// </summary>
        public QslMsgIntlTag() { }

        /// <summary>
        /// Creates a new QSLMSG_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslMsgIntlTag(string value) : base(value) { }
    }
}
