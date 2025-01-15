
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's antenna.
    /// </summary>
    public class MyAntennaTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyAntenna;

        /// <summary>
        /// Creates a new MY_ANTENNA tag.
        /// </summary>
        public MyAntennaTag() { }

        /// <summary>
        /// Creates a new MY_ANTENNA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyAntennaTag(string value) : base(value) { }
    }
}
