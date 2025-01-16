
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the ARRL Logbook of the World QSL received status.
    /// </summary>
    public class LotwQslRcvdTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.LotwQslRcvdStatus;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.QslReceivedStatuses;

        /// <summary>
        /// Creates a new LOTW_QSL_RCVD tag.
        /// </summary>
        public LotwQslRcvdTag() { }

        /// <summary>
        /// Creates a new LOTW_QSL_RCVD tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public LotwQslRcvdTag(string value) : base(value) { }
    }
}
