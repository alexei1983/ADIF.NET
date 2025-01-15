
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's IOTA designator.
    /// </summary>
    public class MyIotaTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyIota;

        /// <summary>
        /// Creates a new MY_IOTA tag.
        /// </summary>
        public MyIotaTag() { }

        /// <summary>
        /// Creates a new MY_IOTA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyIotaTag(string value) : base(value) { }

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
