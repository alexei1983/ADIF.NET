
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the name of the contacted station's special activity or interest group.
    /// </summary>
    public class SigTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Sig;

        /// <summary>
        /// Creates a new SIG tag.
        /// </summary>
        public SigTag() { }

        /// <summary>
        /// Creates a new SIG tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SigTag(string value) : base(value) { }
    }
}
