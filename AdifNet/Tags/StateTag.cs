
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the code for the logging station's Primary Administrative Subdivision (e.g. US State, JA Island, VE Province).
    /// </summary>
    public class StateTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.State;

        /// <summary>
        /// Primary subdivision enumeration.
        /// </summary>
        public override AdifEnumeration Options => Values.PrimarySubdivisions;

        /// <summary>
        /// Creates a new STATE tag.
        /// </summary>
        public StateTag() { }

        /// <summary>
        /// Creates a new STATE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public StateTag(string value) : base(value) { }
    }
}
