
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's WPX prefix.
    /// </summary>
    public class PfxTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Pfx;

        /// <summary>
        /// Creates a new PFX tag.
        /// </summary>
        public PfxTag() { }

        /// <summary>
        /// Creates a new PFX tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public PfxTag(string value) : base(value) { }
    }
}
