
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the QSL card message.
    /// </summary>
    public class QslMsgTag : MultilineStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslMsg;

        /// <summary>
        /// Creates a new QSLMSG tag.
        /// </summary>
        public QslMsgTag() { }

        /// <summary>
        /// Creates a new QSLMSG tag.
        /// </summary>
        /// <param name="value">QSL card message.</param>
        public QslMsgTag(string value) : base(value) { }
    }
}
