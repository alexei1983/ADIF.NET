
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the callsign of the logging operator.
    /// </summary>
    public class OperatorTag : BaseCallSignTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Operator;

        /// <summary>
        /// Creates a new OPERATOR tag.
        /// </summary>
        public OperatorTag() { }

        /// <summary>
        /// Creates a new OPERATOR tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public OperatorTag(string value) : base(value) { }
    }
}
