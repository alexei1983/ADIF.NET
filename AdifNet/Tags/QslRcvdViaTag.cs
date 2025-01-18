
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the means by which the QSL was received by the logging station.
    /// </summary>
    public class QslRcvdViaTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslRcvdVia;

        /// <summary>
        /// Valid enumeration options.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.Via;

        /// <summary>
        /// Creates a new QSL_RCVD_VIA tag.
        /// </summary>
        public QslRcvdViaTag() { }

        /// <summary>
        /// Creates a new QSL_RCVD_VIA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslRcvdViaTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new QSL_RCVD_VIA tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public QslRcvdViaTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
