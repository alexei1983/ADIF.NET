
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's Secondary Administrative Subdivision.
    /// </summary>
    public class MyCntyTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyCnty;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.SecondarySubdivisions;

        /// <summary>
        /// Creates a new MY_CNTY tag.
        /// </summary>
        public MyCntyTag() { }

        /// <summary>
        /// Creates a new MY_CNTY tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyCntyTag(string value) : base(value) { }
    }
}

