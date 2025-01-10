
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's Secondary Administrative Subdivision.
    /// </summary>
    public class CntyTag : RestrictedEnumerationTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Cnty;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.SecondarySubdivisions;

        /// <summary>
        /// Creates a new CNTY tag.
        /// </summary>
        public CntyTag() { }

        /// <summary>
        /// Creates a new CNTY tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CntyTag(string value) : base(value) { }
    }
}
