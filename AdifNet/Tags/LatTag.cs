using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's latitude.
    /// </summary>
    public class LatTag : LocationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Lat;

        /// <summary>
        /// Creates a new LAT tag.
        /// </summary>
        public LatTag() { }

        /// <summary>
        /// Creates a new LAT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public LatTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new LAT tag.
        /// </summary>
        /// <param name="latitude">Decimal latitude.</param>
        public LatTag(decimal latitude) : base(latitude, LocationType.Latitude) { }

        /// <summary>
        /// Creates a new LAT tag.
        /// </summary>
        /// <param name="location">Initial tag value.</param>
        public LatTag(Location location) : base(location) { }
    }
}
