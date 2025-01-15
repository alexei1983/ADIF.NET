
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's DXCC entity name.
    /// </summary>
    public class CountryTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Country;

        /// <summary>
        /// Creates a new COUNTRY tag.
        /// </summary>
        public CountryTag() { }

        /// <summary>
        /// Creates a new COUNTRY tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CountryTag(string value) : base(value) { }
    }
}
