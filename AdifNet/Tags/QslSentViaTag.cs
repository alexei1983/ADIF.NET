
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the means by which the QSL was sent by the logging station.
    /// </summary>
    public class QslSentViaTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslSentVia;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.Via;

        /// <summary>
        /// Creates a new QSL_SENT_VIA tag.
        /// </summary>
        public QslSentViaTag() { }

        /// <summary>
        /// Creates a new QSL_SENT_VIA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslSentViaTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QSL_SENT_VIA tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QslSentViaTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
