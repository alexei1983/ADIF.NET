
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's IOTA designator.
    /// </summary>
    public class IotaTag : StringTag, ITag
    {
        /// <summary>
        /// Name of the tag.
        /// </summary>
        public override string Name => AdifTags.Iota;

        /// <summary>
        /// Creates a new IOTA tag.
        /// </summary>
        public IotaTag() { }

        /// <summary>
        /// Creates a new IOTA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public IotaTag(string value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            return base.ValidateValue(value) && (value?.ToString() ?? string.Empty).IsIotaDesignator();
        }
    }
}
