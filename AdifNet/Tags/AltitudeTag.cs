
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the height of the contacted station in meters relative to Mean Sea Level (MSL).
    /// </summary>
    public class AltitudeTag : NumberTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Altitude;

        /// <summary>
        /// Creates a new ALTITUDE tag.
        /// </summary>
        public AltitudeTag() { }

        /// <summary>
        /// Creates a new ALTITUDE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public AltitudeTag(double value) : base(value) { }
    }
}
