
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the contacted station's URL.
    /// </summary>
    public class WebTag : StringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Web;

        /// <summary>
        /// Creates a new WEB tag.
        /// </summary>
        public WebTag() { }

        /// <summary>
        /// Creates a new WEB tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public WebTag(string value) : base(value) { }
    }
}
