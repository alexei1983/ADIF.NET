using System;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the time the QSO started.
    /// </summary>
    public class TimeOnTag : TimeTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.TimeOn;

        /// <summary>
        /// Creates a new TIME_ON tag.
        /// </summary>
        public TimeOnTag() { }

        /// <summary>
        /// Creates a new TIME_ON tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public TimeOnTag(DateTime value) : base(value) { }
    }
}
