
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the mode of the QSO.
    /// </summary>
    public class SubModeTag : EnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SubMode;

        /// <summary>
        /// Valid enumeration options.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.SubModes;

        /// <summary>
        /// Whether or not values must be part of the defined enumeration for the tag.
        /// </summary>
        public override bool RestrictOptions => false;

        /// <summary>
        /// Creates a new SUBMODE tag.
        /// </summary>
        public SubModeTag() { }

        /// <summary>
        /// Creates a new SUBMODE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SubModeTag(string value) : base(value) { }
    }
}
