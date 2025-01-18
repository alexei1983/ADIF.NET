
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSL sent status for DARC Community Logbook (DCL).
    /// </summary>
    public class DclQslSentTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.DclQslSent;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.QslSentStatuses;

        /// <summary>
        /// Creates a new DCL_QSL_SENT tag.
        /// </summary>
        public DclQslSentTag() { }

        /// <summary>
        /// Creates a new DCL_QSL_SENT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DclQslSentTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new DCL_QSL_SENT tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public DclQslSentTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
