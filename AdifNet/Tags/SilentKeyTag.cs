
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Whether or not the contacted station's operator is now a silent key.
    /// </summary>
    public class SilentKeyTag : BooleanTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SilentKey;

        /// <summary>
        /// Creates a new SILENT_KEY tag.
        /// </summary>
        public SilentKeyTag() { }

        /// <summary>
        /// Creates a new SILENT_KEY tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SilentKeyTag(bool value) : base(value) { }
    }
}
