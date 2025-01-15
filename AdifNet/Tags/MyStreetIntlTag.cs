
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's street.
    /// </summary>
    public class MyStreetIntlTag : IntlStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyStreetIntl;

        /// <summary>
        /// Creates a new MY_STREET_INTL tag.
        /// </summary>
        public MyStreetIntlTag() { }

        /// <summary>
        /// Creates a new MY_STREET_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyStreetIntlTag(string value) : base(value) { }
    }
}
