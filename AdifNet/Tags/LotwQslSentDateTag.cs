using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the date the QSL was sent to ARRL Logbook of the World.
    /// </summary>
    public class LotwQslSentDateTag : DateTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.LotwQslSentDate;

        /// <summary>
        /// Creates a new LOTW_QSLSDATE tag.
        /// </summary>
        public LotwQslSentDateTag() { }

        /// <summary>
        /// Creates a new LOTW_QSLSDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public LotwQslSentDateTag(DateTime value) : base(value) { }

    }
}
