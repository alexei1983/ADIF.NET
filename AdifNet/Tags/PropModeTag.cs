﻿
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the propagation mode for the QSO.
    /// </summary>
    public class PropModeTag : RestrictedEnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.PropMode;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => AdifEnumerations.PropagationModes;

        /// <summary>
        /// Creates a new PROP_MODE tag.
        /// </summary>
        public PropModeTag() { }

        /// <summary>
        /// Creates a new PROP_MODE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public PropModeTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new PROP_MODE tag.
        /// </summary>
        /// <param name="enumValue">Initial tag value.</param>
        public PropModeTag(AdifEnumerationValue enumValue) : base(enumValue) { }
    }
}
