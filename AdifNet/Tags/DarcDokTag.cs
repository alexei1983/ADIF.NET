
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's DARC DOK (District Location Code).
    /// </summary>
    public class DarcDokTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.DarcDok;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.DarcDoks;

        /// <summary>
        /// Creates a new DARC_DOK tag.
        /// </summary>
        public DarcDokTag() { }

        /// <summary>
        /// Creates a new DARC_DOK tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public DarcDokTag(string value) : base(value) { }
    }
}
