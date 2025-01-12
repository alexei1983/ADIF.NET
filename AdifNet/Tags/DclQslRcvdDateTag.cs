
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the date the QSL was received from DARC Community Logbook (DCL).
    /// </summary>
    public class DclQslRcvdDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.DclQslRcvdDate;

        /// <summary>
        /// Creates a new DCL_QSLRDATE tag.
        /// </summary>
        public DclQslRcvdDateTag() { }

        /// <summary>
        /// Creates a new DCL_QSLRDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DclQslRcvdDateTag(DateTime value) : base(value) { }
    }
}
