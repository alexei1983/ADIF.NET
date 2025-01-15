
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Tag that marks the end of the header in an ADIF data set.
    /// </summary>
    public class EndHeaderTag : ValuelessTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.EndHeader;

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;
    }
}
