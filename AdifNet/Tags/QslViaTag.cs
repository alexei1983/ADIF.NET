
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contacted station's QSL route.
    /// </summary>
    public class QslViaTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.QslVia;

        /// <summary>
        /// Creates a new QSL_VIA tag.
        /// </summary>
        public QslViaTag() { }

        /// <summary>
        /// Creates a new QSL_VIA tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QslViaTag(string value) : base(value) { }
    }
}
