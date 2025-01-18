
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents a comma-delimited list of one or more of the contacted station's POTA (Parks on the Air) reference(s).
    /// </summary>
    public class PotaRefTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.PotaRef;

        /// <summary>
        /// Value separator.
        /// </summary>
        public override string ValueSeparator => AdifConstants.Comma.ToString();

        /// <summary>
        /// Creates a new POTA_REF tag.
        /// </summary>
        public PotaRefTag() { }

        /// <summary>
        /// Creates a new POTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public PotaRefTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new POTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public PotaRefTag(params string[] value) : base(value) { }
    }
}
