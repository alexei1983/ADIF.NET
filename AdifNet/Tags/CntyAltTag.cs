
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's Secondary Administrative Subdivision alternate.
    /// </summary>
    public class CntyAltTag : MultiValueEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.CntyAlt;

        /// <summary>
        /// Valid enumeration values.
        /// TODO: Fix this.
        /// </summary>
        public override AdifEnumeration Options => Values.SecondarySubdivisionAlts;

        /// <summary>
        /// ADIF type.
        /// </summary>
        public override IAdifType AdifType => new AdifSecondarySubdivisionListAlt();

        /// <summary>
        /// Creates a new CNTY_ALT tag.
        /// </summary>
        public CntyAltTag() { }

        /// <summary>
        /// Creates a new CNTY_ALT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CntyAltTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new CNTY_ALT tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public CntyAltTag(AdifEnumerationValue value) : base(value) { }
    }
}
