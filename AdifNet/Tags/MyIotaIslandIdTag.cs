
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging station's IOTA Island Identifier.
    /// </summary>
    public class MyIotaIslandIdTag : PositiveIntegerTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyIotaIslandId;

        /// <summary>
        /// Creates a new MY_IOTA_ISLAND_ID tag.
        /// </summary>
        public MyIotaIslandIdTag() { }

        /// <summary>
        /// Creates a new MY_IOTA_ISLAND_ID tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyIotaIslandIdTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new MY_IOTA_ISLAND_ID tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyIotaIslandIdTag(int value) : base(value) { }
    }
}
