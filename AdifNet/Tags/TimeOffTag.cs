
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the time the QSO ended.
    /// </summary>
    public class TimeOffTag : TimeTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.TimeOff;

        /// <summary>
        /// Creates a new TIME_OFF tag.
        /// </summary>
        public TimeOffTag() { }

        /// <summary>
        /// Creates a new TIME_OFF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public TimeOffTag(DateTime value) : base(value) { }
    }
}
