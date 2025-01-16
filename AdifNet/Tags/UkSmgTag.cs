
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
    /// </summary>
    public class UkSmgTag : PositiveIntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.UkSmg;

        /// <summary>
        /// Creates a new UKSMG tag.
        /// </summary>
        public UkSmgTag() { }

        /// <summary>
        /// Creates a new UKSMG tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public UkSmgTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new UKSMG tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public UkSmgTag(int value) : base(value) { }
    }
}
