
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// 
    /// </summary>
    public class NotesIntlTag : IntlMultilineStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.NotesIntl;

        /// <summary>
        /// Creates a new NOTES_INTL tag.
        /// </summary>
        public NotesIntlTag() { }

        /// <summary>
        /// Creates a new NOTES_INTL tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public NotesIntlTag(string value) : base(value) { }
    }
}
