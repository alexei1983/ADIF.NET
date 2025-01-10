
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Indicates whether or not the QSO pertains to a shortwave listener (SWL) report.
    /// </summary>
    public class SwlTag : BooleanTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Swl;

        /// <summary>
        /// Creates a new SWL tag.
        /// </summary>
        public SwlTag() { }

        /// <summary>
        /// Creates a new SWL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public SwlTag(bool value) : base(value) { }
    }
}
