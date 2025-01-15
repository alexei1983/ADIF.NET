
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's CQ zone.
    /// </summary>
    public class CQZTag : PositiveIntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Cqz;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override int MinValue => 1;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override int MaxValue => 40;

        /// <summary>
        /// Creates a new CQZ tag.
        /// </summary>
        public CQZTag() { }

        /// <summary>
        /// Creates a new CQZ tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CQZTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new CQZ tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CQZTag(int value) : base(value) { }
    }
}
