
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents the contest precedence (e.g. for ARRL sweepstakes).
    /// </summary>
    public class PrecedenceTag : StringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.Precedence;

        /// <summary>
        /// Enumeration values.
        /// </summary>
        public override AdifEnumeration Options => Values.ArrlPrecedence;

        /// <summary>
        /// Creates a new PRECEDENCE tag.
        /// </summary>
        public PrecedenceTag() { }

        /// <summary>
        /// Creates a new PRECEDENCE tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public PrecedenceTag(string value) : base(value) { }
    }
}
