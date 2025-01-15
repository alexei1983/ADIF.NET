

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSL was received from ARRL Logbook of the World.
    /// </summary>
    public class LotwQslReceivedDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.LotwQslReceivedDate;

        /// <summary>
        /// Creates a new LOTW_QSLRDATE tag.
        /// </summary>
        public LotwQslReceivedDateTag() { }

        /// <summary>
        /// Creates a new LOTW_QSLRDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public LotwQslReceivedDateTag(DateTime value) : base(value) { }

    }
}
