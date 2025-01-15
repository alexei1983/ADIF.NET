
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL sent date.
    /// </summary>
    public class QslSentDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslSentDate;

        /// <summary>
        /// Creates a new QSLSDATE tag.
        /// </summary>
        public QslSentDateTag() { }

        /// <summary>
        /// Creates a new QSLSDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslSentDateTag(DateTime value) : base(value) { }
    }
}
