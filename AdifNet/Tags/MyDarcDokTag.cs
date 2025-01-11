
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's DARC DOK (District Location Code).
    /// </summary>
    public class MyDarcDokTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyDarcDok;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.DarcDoks;

        /// <summary>
        /// Creates a new MY_DARC_DOK tag.
        /// </summary>
        public MyDarcDokTag() { }

        /// <summary>
        /// Creates a new MY_DARC_DOK tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyDarcDokTag(string value) : base(value) { }
    }
}
