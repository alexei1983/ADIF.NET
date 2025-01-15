
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's ITU zone.
    /// </summary>
    public class MyItuZoneTag : PositiveIntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyItuZone;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override int MinValue => 1;

        /// <summary>
        /// Maximum numeric value.
        /// </summary>
        public override int MaxValue => 90;

        /// <summary>
        /// Creates a new MY_ITU_ZONE tag.
        /// </summary>
        public MyItuZoneTag() { }

        /// <summary>
        /// Creates a new MY_ITU_ZONE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyItuZoneTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new MY_ITU_ZONE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyItuZoneTag(int value) : base(value) { }
    }
}
