
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents two US counties where the contacted station is located on a border between two counties.
    /// </summary>
    public class UsacaCountiesTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.UsacaCounties;

        /// <summary>
        /// String that delimits values in a multi-valued tag.
        /// </summary>
        public override string ValueSeparator => Values.COLON.ToString();

        /// <summary>
        /// Maximum number of values allowed in a multi-valued tag.
        /// </summary>
        public override int MaxValueCount => 2;

        /// <summary>
        /// Minimum number of values allowed in a multi-valued tag.
        /// </summary>
        public override int MinValueCount => 2;

        /// <summary>
        /// Creates a new USACA_COUNTIES tag.
        /// </summary>
        public UsacaCountiesTag() : base() { }

        /// <summary>
        /// Creates a new USACA_COUNTIES tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public UsacaCountiesTag(string value) : base(value)
        {
        }
    }
}
