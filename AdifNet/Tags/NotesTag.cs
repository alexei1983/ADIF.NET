
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// 
    /// </summary>
    public class NotesTag : MultilineStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Notes;

        /// <summary>
        /// Creates a new NOTES tag.
        /// </summary>
        public NotesTag() { }

        /// <summary>
        /// Creates a new NOTES tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public NotesTag(string value) : base(value) { }
    }
}
