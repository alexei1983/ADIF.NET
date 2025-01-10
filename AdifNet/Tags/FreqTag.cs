
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the QSO frequency in Megahertz.
    /// </summary>
    public class FreqTag : NumberTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Freq;

        /// <summary>
        /// Creates a new FREQ tag.
        /// </summary>
        public FreqTag() { }

        /// <summary>
        /// Creates a new FREQ tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public FreqTag(double value) : base(value) { }
    }
}
