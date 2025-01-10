

namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's FISTS CW Club Century Certificate (CC) number.
    /// </summary>
    public class FistsCcTag : PositiveIntegerTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.FistsCc;

        /// <summary>
        /// Creates a new FISTS_CC tag.
        /// </summary>
        public FistsCcTag() { }

        /// <summary>
        /// Creates a new FISTS_CC tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public FistsCcTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new FISTS_CC tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public FistsCcTag(int value) : base(value) { }
    }
}
