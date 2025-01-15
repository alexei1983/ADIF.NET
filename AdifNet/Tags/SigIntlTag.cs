
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the name of the contacted station's special activity or interest group.
    /// </summary>
    public class SigIntlTag : IntlStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SigIntl;

        /// <summary>
        /// Creates a new SIG_INTL tag.
        /// </summary>
        public SigIntlTag() { }

        /// <summary>
        /// Creates a new SIG_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SigIntlTag(string value) : base(value) { }
    }
}
