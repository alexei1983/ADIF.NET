
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's city.
    /// </summary>
    public class QthTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Qth;

        /// <summary>
        /// Creates a new QTH tag.
        /// </summary>
        public QthTag() { }

        /// <summary>
        /// Creates a new QTH tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public QthTag(string value) : base(value) { }
    }
}
