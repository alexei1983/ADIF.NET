
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date the QSL was sent to eQSL.cc.
    /// </summary>
    public class EQslSentDateTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.EQslSentDate;

        /// <summary>
        /// Creates a new EQSL_QSLSDATE tag.
        /// </summary>
        public EQslSentDateTag() { }

        /// <summary>
        /// Creates a new EQSL_QSLSDATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public EQslSentDateTag(DateTime value) : base(value) { }
    }
}
