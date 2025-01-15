
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's city.
    /// </summary>
    public class QthIntlTag : IntlStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QthIntl;

        /// <summary>
        /// Creates a new QTH_INTL tag.
        /// </summary>
        public QthIntlTag() { }

        /// <summary>
        /// Creates a new QTH_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QthIntlTag(string value) : base(value) { }
    }
}
