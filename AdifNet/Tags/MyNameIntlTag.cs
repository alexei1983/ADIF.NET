
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the logging operator's name.
    /// </summary>
    public class MyNameIntlTag : IntlStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyNameIntl;

        /// <summary>
        /// Creates a new MY_NAME_INTL tag.
        /// </summary>
        public MyNameIntlTag() { }

        /// <summary>
        /// Creates a new MY_NAME_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyNameIntlTag(string value) : base(value) { }
    }
}
