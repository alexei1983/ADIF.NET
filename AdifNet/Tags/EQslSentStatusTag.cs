
namespace org.goodspace.Data.Radio.Adif.Tags
{ 
    /// <summary>
    /// Represents the eQSL.cc QSL sent status.
    /// </summary>
    public class EQslSentStatusTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.EQslSentStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.EQslSentStatuses;

        /// <summary>
        /// Creates a new EQSL_QSL_SENT tag.
        /// </summary>
        public EQslSentStatusTag() { }

        /// <summary>
        /// Creates a new EQSL_QSL_SENT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public EQslSentStatusTag(string value) : base(value) { }
    }
}
