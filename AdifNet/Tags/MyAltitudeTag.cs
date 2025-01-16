
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the height of the logging station in meters relative to Mean Sea Level (MSL).
    /// </summary>
    public class MyAltitudeTag : NumberTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyAltitude;

        /// <summary>
        /// Creates a new MY_ALTITUDE tag.
        /// </summary>
        public MyAltitudeTag() { }

        /// <summary>
        /// Creates a new MY_ALTITUDE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyAltitudeTag(double value) : base(value) { }
    }
}
