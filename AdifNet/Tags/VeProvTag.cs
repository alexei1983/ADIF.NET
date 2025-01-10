using org.goodspace.Data.Radio.Adif.Attributes;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the two-letter Canadian province or territory abbreviation of the contacted station.
    /// </summary>
    [DeprecatedTag(AdifTags.State)]
    public class VeProvTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.VeProv;

        /// <summary>
        /// Creates a new VE_PROV tag.
        /// </summary>
        public VeProvTag() { }

        /// <summary>
        /// Creates a new VE_PROV tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public VeProvTag(string value) : base(value) { }
    }
}
