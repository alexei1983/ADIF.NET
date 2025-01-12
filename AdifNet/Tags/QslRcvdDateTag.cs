
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL received date.
    /// </summary>
    public class QslRcvdDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslRcvdDate;

        /// <summary>
        /// Creates a new QSLRDATE tag.
        /// </summary>
        public QslRcvdDateTag() { }

        /// <summary>
        /// Creates a new QSLRDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslRcvdDateTag(DateTime value) : base(value) { }

    }
}
