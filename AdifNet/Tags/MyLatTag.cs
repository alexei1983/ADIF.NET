using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's latitude.
    /// </summary>
    public class MyLatTag : LocationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyLat;

        /// <summary>
        /// Creates a new MY_LAT tag.
        /// </summary>
        public MyLatTag() { }

        /// <summary>
        /// Creates a new MY_LAT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyLatTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new MY_LAT tag.
        /// </summary>
        /// <param name="latitude">Decimal latitude.</param>
        public MyLatTag(decimal latitude) : base(latitude, LocationType.Latitude) { }

        /// <summary>
        /// Creates a new MY_LAT tag.
        /// </summary>
        /// <param name="location">Initial tag value.</param>
        public MyLatTag(Location location) : base(location) { }
    }
}
