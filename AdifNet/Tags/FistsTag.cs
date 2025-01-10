
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's FISTS CW Club member information.
    /// </summary>
    public class FistsTag : PositiveIntegerTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Fists;

        /// <summary>
        /// Creates a new FISTS tag.
        /// </summary>
        public FistsTag() { }

        /// <summary>
        /// Creates a new FISTS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public FistsTag(double value) : base(value) { }

        /// <summary>
        /// Creates a new FISTS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public FistsTag(int value) : base(value) { }
    }
}

