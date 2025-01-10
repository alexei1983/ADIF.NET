using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the date the QSL was received from eQSL.cc.
    /// </summary>
    public class EQslReceivedDateTag : DateTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.EQslReceivedDate;

        /// <summary>
        /// Creates a new EQSL_QSLRDATE tag.
        /// </summary>
        public EQslReceivedDateTag() { }

        /// <summary>
        /// Creates a new EQSL_QSLRDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public EQslReceivedDateTag(DateTime value) : base(value) { }
    }
}
