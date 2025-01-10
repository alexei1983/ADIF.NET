
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's power in watts.
    /// </summary>
    public class TxPwrTag : NumberTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.TxPwr;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => 1;

        /// <summary>
        /// Creates a new TX_PWR tag.
        /// </summary>
        public TxPwrTag() { }

        /// <summary>
        /// Creates a new TX_PWR tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public TxPwrTag(double value) : base(value) { }
    }
}
