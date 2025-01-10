
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging operator's name.
    /// </summary>
    public class MyNameTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyName;

        /// <summary>
        /// Creates a new MY_NAME tag.
        /// </summary>
        public MyNameTag() { }

        /// <summary>
        /// Creates a new MY_NAME tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyNameTag(string value) : base(value) { }
    }
}
