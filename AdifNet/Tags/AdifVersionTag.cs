
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the version of ADIF used to generate the data set.
    /// </summary>
    public class AdifVersionTag : BaseVersionTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.AdifVer;

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;

        /// <summary>
        /// Creates a new ADIF_VER tag.
        /// </summary>
        public AdifVersionTag()
        {
        }

        /// <summary>
        /// Creates a new ADIF_VER tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public AdifVersionTag(Version value) : base(value)
        {
        }
    }
}
