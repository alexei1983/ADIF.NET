using org.goodspace.Data.Radio.Adif.Attributes;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging operator's callsign.
    /// </summary>
    [DeprecatedTag(AdifTags.Operator)]
    public class GuestOpTag : BaseCallSignTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.GuestOp;

        /// <summary>
        /// Creates a new GUEST_OP tag.
        /// </summary>
        public GuestOpTag() { }

        /// <summary>
        /// Creates a new GUEST_OP tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public GuestOpTag(string value) : base(value) { }
    }
}
