

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the date on which the QSO ended.
    /// </summary>
    public class QsoDateOffTag : DateTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QsoDateOff;

        /// <summary>
        /// Creates a new QSO_DATE_OFF tag.
        /// </summary>
        public QsoDateOffTag() { }

        /// <summary>
        /// Creates a new QSO_DATE_OFF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QsoDateOffTag(DateTime value) : base(value) { }
    }
}
