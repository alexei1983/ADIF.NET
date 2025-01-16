
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Identifies the version of the logger, converter, or utility that created or processed the ADIF data set.
    /// </summary>
    public class ProgramVersionTag : BaseVersionTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ProgramVersion;

        /// <summary>
        /// Whether or not the tag is a header tag.
        /// </summary>
        public override bool Header => true;

        /// <summary>
        /// Creates a new PROGRAMVERSION tag.
        /// </summary>
        public ProgramVersionTag()
        {
        }

        /// <summary>
        /// Creates a new PROGRAMVERSION tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ProgramVersionTag(Version value) : base(value)
        {
        }
    }
}
