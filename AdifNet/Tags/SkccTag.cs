
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's Straight Key Century Club (SKCC) member information.
    /// </summary>
    public class SkccTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Skcc;

        /// <summary>
        /// Creates a new SKCC tag.
        /// </summary>
        public SkccTag() { }

        /// <summary>
        /// Creates a new SKCC tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SkccTag(string value) : base(value) { }
    }
}
