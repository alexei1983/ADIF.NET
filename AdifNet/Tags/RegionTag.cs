
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// 
    /// </summary>
    public class RegionTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Region;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.Regions;

        /// <summary>
        /// Creates a new REGION tag.
        /// </summary>
        public RegionTag() { }

        /// <summary>
        /// Creates a new REGION tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RegionTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new REGION tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public RegionTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
