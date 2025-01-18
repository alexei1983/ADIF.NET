
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents two US counties where the contacted station is located on a border between two counties.
    /// </summary>
    public class MyUsacaCountiesTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyUsacaCounties;

        /// <summary>
        /// String that delimits values in a multi-valued tag.
        /// </summary>
        public override string ValueSeparator => AdifConstants.Colon.ToString();

        /// <summary>
        /// Maximum number of values in a multi-valued tag.
        /// </summary>
        public override int MaxValueCount => 2;

        /// <summary>
        /// Minimum number of values in a multi-valued tag.
        /// </summary>
        public override int MinValueCount => 2;

        /// <summary>
        /// Creates a new MY_USACA_COUNTIES tag.
        /// </summary>
        public MyUsacaCountiesTag() : base() { }

        /// <summary>
        /// Creates a new MY_USACA_COUNTIES tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyUsacaCountiesTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new MY_USACA_COUNTIES tag.
        /// </summary>
        /// <param name="values">Initial tag values.</param>
        public MyUsacaCountiesTag(params string[] values) : base(values) { }
    }
}
