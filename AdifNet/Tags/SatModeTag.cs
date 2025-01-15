
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the satellite mode.
    /// </summary>
    public class SatModeTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SatMode;

        /// <summary>
        /// Creates a new SAT_MODE tag.
        /// </summary>
        public SatModeTag() { }

        /// <summary>
        /// Creates a new SAT_MODE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SatModeTag(string value) : base(value) { }
    }
}
