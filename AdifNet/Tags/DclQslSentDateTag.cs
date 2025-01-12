
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSL was sent to DARC Community Logbook (DCL).
    /// </summary>
    public class DclQslSentDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.DclQslSentDate;

        /// <summary>
        /// Creates a new DCL_QSLSDATE tag.
        /// </summary>
        public DclQslSentDateTag() { }

        /// <summary>
        /// Creates a new DCL_QSLSDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DclQslSentDateTag(DateTime value) : base(value) { }
    }
}
