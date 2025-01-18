
namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// Represents two or four adjacent Maidenhead grid locators for the contacted station's grid squares 
    /// credited to the QSO for the ARRL VUCC award program.
    /// </summary>
    public class VuccGridsTag : MultiValueStringTag, ITag
    {
        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.VuccGrids;

        /// <summary>
        /// String that delimits values in a multi-valued tag.
        /// </summary>
        public override string ValueSeparator => AdifConstants.Comma.ToString();

        /// <summary>
        /// Maximum number of allowed values.
        /// </summary>
        public override int MaxValueCount => 4;

        /// <summary>
        /// Minimum number of allowed values.
        /// </summary>
        public override int MinValueCount => 2;

        /// <summary>
        /// Creates a new VUCC_GRIDS tag.
        /// </summary>
        public VuccGridsTag() : base() { }

        /// <summary>
        /// Creates a new VUCC_GRIDS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public VuccGridsTag(string value) : base(value) { }

        /// <summary>
        /// Creates a new VUCC_GRIDS tag.
        /// </summary>
        /// <param name="values">Initial tag values.</param>
        public VuccGridsTag(params string[] values) : base(values) { }
    }
}
