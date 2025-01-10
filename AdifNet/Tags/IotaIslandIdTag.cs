
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's IOTA Island Identifier.
    /// </summary>
    public class IotaIslandIdTag : PositiveIntegerTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.IotaIslandId;

        /// <summary>
        /// Creates a new IOTA_ISLAND_ID tag.
        /// </summary>
        public IotaIslandIdTag() { }

        /// <summary>
        /// Creates a new IOTA_ISLAND_ID tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public IotaIslandIdTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new IOTA_ISLAND_ID tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public IotaIslandIdTag(int value) : base(value) { }
    }
}
