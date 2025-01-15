
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's city.
    /// </summary>
    public class MyCityIntlTag : IntlStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyCityIntl;

        /// <summary>
        /// Creates a new MY_CITY_INTL tag.
        /// </summary>
        public MyCityIntlTag() { }

        /// <summary>
        /// Creates a new MY_CITY_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyCityIntlTag(string value) : base(value) { }
    }
}
