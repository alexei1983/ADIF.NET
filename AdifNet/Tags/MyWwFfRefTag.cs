
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's WWFF reference.
    /// </summary>
    public class MyWwFfRefTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyWwFfRef;

        /// <summary>
        /// Creates a new MY_WWFF_REF tag.
        /// </summary>
        public MyWwFfRefTag() { }

        /// <summary>
        /// Creates a new MY_WWFF_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyWwFfRefTag(string value) : base(value) { }
    }
}
