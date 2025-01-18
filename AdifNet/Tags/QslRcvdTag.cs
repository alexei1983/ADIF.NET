
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL received status.
    /// </summary>
    public class QslRcvdTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslRcvd;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QslReceivedStatuses;

        /// <summary>
        /// Creates a new QSL_RCVD tag.
        /// </summary>
        public QslRcvdTag() { }

        /// <summary>
        /// Creates a new QSL_RCVD tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslRcvdTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QSL_RCVD tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QslRcvdTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
