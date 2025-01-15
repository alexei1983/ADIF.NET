
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// 
    /// </summary>
    public class SotaRefTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SotaRef;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifSotaRef();

        /// <summary>
        /// Creates a new SOTA_REF tag.
        /// </summary>
        public SotaRefTag() { }

        /// <summary>
        /// Creates a new SOTA_REF tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SotaRefTag(string value) : base(value) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override bool ValidateValue(object? value)
        {
            return AdifType.TryParse(value is null ? string.Empty : value.ToString(), out _);
        }
    }
}
