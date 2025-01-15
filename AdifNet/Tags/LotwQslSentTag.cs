
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the ARRL Logbook of the World QSL sent status.
    /// </summary>
    public class LotwQslSentTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.LotwQslSentStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.QslSentStatuses;

        /// <summary>
        /// Creates a new LOTW_QSL_SENT tag.
        /// </summary>
        public LotwQslSentTag() { }

        /// <summary>
        /// Creates a new LOTW_QSL_SENT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public LotwQslSentTag(string value) : base(value) { }
    }
}
