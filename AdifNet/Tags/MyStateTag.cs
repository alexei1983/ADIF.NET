
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the code for the logging station's Primary Administrative Subdivision (e.g. US State, JA Island, VE Province).
    /// </summary>
    public class MyStateTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyState;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.PrimarySubdivisions;

        /// <summary>
        /// Creates a new MY_STATE tag.
        /// </summary>
        public MyStateTag() { }

        /// <summary>
        /// Creates a new MY_STATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyStateTag(string value) : base(value) { }
    }
}
