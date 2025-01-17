﻿
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the name of the satellite.
    /// </summary>
    public class SatNameTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.SatName;

        /// <summary>
        /// Creates a new SAT_NAME tag.
        /// </summary>
        public SatNameTag() { }

        /// <summary>
        /// Creates a new SAT_NAME tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SatNameTag(string value) : base(value) { }
    }
}
