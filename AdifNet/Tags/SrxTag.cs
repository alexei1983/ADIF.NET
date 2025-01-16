
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contest QSO received serial number.
    /// </summary>
    public class SrxTag : IntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Srx;

        /// <summary>
        /// Creates a new SRX tag.
        /// </summary>
        public SrxTag() { }

        /// <summary>
        /// Creates a new SRX tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SrxTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new SRX tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SrxTag(int value) : base(value) { }
    }
}
