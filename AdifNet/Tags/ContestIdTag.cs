
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents the QSO contest identifier.
    /// </summary>
    public class ContestIdTag : EnumerationTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.ContestId;

        /// <summary>
        /// Valid enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.Contests;

        /// <summary>
        /// Creates a new CONTEST_ID tag.
        /// </summary>
        public ContestIdTag() { }

        /// <summary>
        /// Creates a new CONTEST_ID tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public ContestIdTag(string value) : base(value) { }
    }
}
