
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the QSL sent status.
    /// </summary>
    public class QslSentTag : RestrictedEnumerationTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslSent;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.QslSentStatuses;

        /// <summary>
        /// Creates a new QSL_SENT tag.
        /// </summary>
        public QslSentTag() { }

        /// <summary>
        /// Creates a new QSL_SENT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslSentTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QSL_SENT tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QslSentTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
