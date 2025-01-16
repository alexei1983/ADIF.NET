
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents a comma-delimited list of one or more of the logging station's POTA (Parks on the Air) reference(s).
    /// </summary>
    public class MyPotaRefTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyPotaRef;

        /// <summary>
        /// Value separator.
        /// </summary>
        public override string ValueSeparator => Values.COMMA.ToString();

        /// <summary>
        /// Creates a new MY_POTA_REF tag.
        /// </summary>
        public MyPotaRefTag() { }

        /// <summary>
        /// Creates a new MY_POTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyPotaRefTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new MY_POTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyPotaRefTag(params string[] value) : base(value) { }
    }
}
