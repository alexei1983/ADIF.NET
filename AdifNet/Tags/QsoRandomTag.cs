
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Indicates whether the QSO was random or scheduled.
    /// </summary>
    public class QsoRandomTag : BooleanTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QsoRandom;

        /// <summary>
        /// Creates a new QSO_RANDOM tag.
        /// </summary>
        public QsoRandomTag() { }

        /// <summary>
        /// Creates a new QSO_RANDOM tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QsoRandomTag(bool value) : base(value) { }
    }
}
