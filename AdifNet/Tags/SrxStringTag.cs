
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contest QSO received serial number.
    /// </summary>
    public class SrxStringTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SrxString;

        /// <summary>
        /// Creates a new SRX_STRING tag.
        /// </summary>
        public SrxStringTag() { }

        /// <summary>
        /// Creates a new SRX_STRING tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SrxStringTag(string value) : base(value) { }
    }
}
