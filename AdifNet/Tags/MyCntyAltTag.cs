
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the logging station's Secondary Administrative Subdivision alternate.
    /// </summary>
    public class MyCntyAltTag : MultiValueEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyCntyAlt;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.SecondarySubdivisionAlts;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifSecondarySubdivisionListAlt();

        /// <summary>
        /// Creates a new MY_CNTY_ALT tag.
        /// </summary>
        public MyCntyAltTag() { }

        /// <summary>
        /// Creates a new MY_CNTY_ALT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyCntyAltTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new MY_CNTY_ALT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyCntyAltTag(AdifEnumerationValue value) : base(value) { }
    }
}
