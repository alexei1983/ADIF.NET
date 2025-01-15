
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's ITU zone.
    /// </summary>
    public class ItuzTag : PositiveIntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Ituz;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override int MinValue => 1;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override int MaxValue => 90;

        /// <summary>
        /// Creates a new ITUZ tag.
        /// </summary>
        public ItuzTag() { }

        /// <summary>
        /// Creates a new ITUZ tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ItuzTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new ITUZ tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ItuzTag(int value) : base(value) { }
    }
}
