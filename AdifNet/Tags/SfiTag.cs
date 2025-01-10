
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the solar flux at the time of the QSO.
    /// </summary>
    public class SfiTag : NumberTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Sfi;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override double MaxValue => 300;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override double MinValue => 0;

        /// <summary>
        /// Creates a new SFI tag.
        /// </summary>
        public SfiTag() { }

        /// <summary>
        /// Creates a new SFI tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SfiTag(double value) : base(value) { }

    }
}
