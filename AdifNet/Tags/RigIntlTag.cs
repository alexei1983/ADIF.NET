
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the description of the contacted station's equipment.
    /// </summary>
    public class RigIntlTag : IntlMultilineStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.RigIntl;

        /// <summary>
        /// Creates a new RIG_INTL tag.
        /// </summary>
        public RigIntlTag() { }

        /// <summary>
        /// Creates a new RIG_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public RigIntlTag(string value) : base(value) { }
    }
}
