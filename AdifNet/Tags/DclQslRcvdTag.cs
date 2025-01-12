
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL received status from DARC Community Logbook (DCL).
    /// </summary>
    public class DclQslRcvdTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.DclQslRcvd;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.QslReceivedStatuses;

        /// <summary>
        /// Creates a new DCL_QSL_RCVD tag.
        /// </summary>
        public DclQslRcvdTag() { }

        /// <summary>
        /// Creates a new DCL_QSL_RCVD tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DclQslRcvdTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new DCL_QSL_RCVD tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public DclQslRcvdTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
