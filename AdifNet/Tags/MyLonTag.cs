using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's longitude.
    /// </summary>
    public class MyLonTag : LocationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyLon;

        /// <summary>
        /// Creates a new MY_LON tag.
        /// </summary>
        public MyLonTag() { }

        /// <summary>
        /// Creates a new MY_LON tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyLonTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new MY_LON tag.
        /// </summary>
        /// <param name="longitude">Decimal longitude.</param>
        public MyLonTag(decimal longitude) : base(longitude, LocationType.Latitude) { }

        /// <summary>
        /// Creates a new MY_LON tag.
        /// </summary>
        /// <param name="location">Initial tag value.</param>
        public MyLonTag(Location location) : base(location) { }
    }
}
