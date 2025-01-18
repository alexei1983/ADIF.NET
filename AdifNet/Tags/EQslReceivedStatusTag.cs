
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the eQSL.cc QSL received status.
    /// </summary>
    public class EQslReceivedStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.EQslReceivedStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.EQslReceivedStatuses;

        /// <summary>
        /// Creates a new EQSL_QSL_RCVD tag.
        /// </summary>
        public EQslReceivedStatusTag() { }

        /// <summary>
        /// Creates a new EQSL_QSL_RCVD tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public EQslReceivedStatusTag(string value) : base(value) { }
    }
}
