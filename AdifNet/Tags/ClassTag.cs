﻿
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contest class (e.g. for ARRL Field Day).
    /// </summary>
    public class ClassTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Class;

        /// <summary>
        /// Creates a new CLASS tag.
        /// </summary>
        public ClassTag() { }

        /// <summary>
        /// Creates a new CLASS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ClassTag(string value) : base(value) { }
    }
}
