
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's WWFF reference.
    /// </summary>
    public class WwFfRefTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.WwFfRef;

        /// <summary>
        /// Creates a new WWFF_REF tag.
        /// </summary>
        public WwFfRefTag() { }

        /// <summary>
        /// Creates a new WWFF_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public WwFfRefTag(string value) : base(value) { }
    }
}
