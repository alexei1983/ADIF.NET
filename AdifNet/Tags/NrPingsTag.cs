
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the number of meteor scatter pings heard by the logging station.
    /// </summary>
    public class NrPingsTag : IntegerTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.NrPings;

        /// <summary>
        /// Minimum numeric value.
        /// </summary>
        public override int MinValue => 0;

        /// <summary>
        /// Creates a new NR_PINGS tag.
        /// </summary>
        public NrPingsTag() { }

        /// <summary>
        /// Creates a new NR_PINGS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public NrPingsTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new NR_PINGS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public NrPingsTag(int value) : base(value) { }
    }
}
