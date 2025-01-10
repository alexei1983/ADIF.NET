using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// 
    /// </summary>
    public class MySotaRefTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MySotaRef;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType ADIFType => new AdifSotaRef();

        /// <summary>
        /// Creates a new MY_SOTA_REF tag.
        /// </summary>
        public MySotaRefTag() { }

        /// <summary>
        /// Creates a new MY_SOTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MySotaRefTag(string value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            return AdifSotaRef.TryParse(value is null ? string.Empty : value.ToString(), out _);
        }
    }
}
