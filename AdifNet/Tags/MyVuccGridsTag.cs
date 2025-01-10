
namespace org.goodspace.Data.Radio.Adif.Tags
{

    /// <summary>
    /// Represents two or four adjacent Maidenhead grid locators for the logging station's grid squares 
    /// that the contacted station may claim for the ARRL VUCC award program.
    /// </summary>
    public class MyVuccGridsTag : MultiValueStringTag, ITag
    {

        /// <summary>
        /// Tag name.
        /// </summary>
        public override string Name => AdifTags.MyVuccGrids;

        /// <summary>
        /// String that delimits values in a multi-valued tag.
        /// </summary>
        public override string ValueSeparator => Values.COMMA.ToString();

        /// <summary>
        /// Maximum number of values in a multi-valued tag.
        /// </summary>
        public override int MaxValueCount => 4;

        /// <summary>
        /// Minimum number of values in a multi-valued tag.
        /// </summary>
        public override int MinValueCount => 2;

        /// <summary>
        /// Creates a new MY_VUCC_GRIDS tag.
        /// </summary>
        public MyVuccGridsTag() : base() { }

        /// <summary>
        /// Creates a new MY_VUCC_GRIDS tag.
        /// </summary>
        /// <param name="value">Initial tag value.</param>
        public MyVuccGridsTag(string value) : base(value)
        {
        }

        /// <summary>
        /// Creates a new MY_VUCC_GRIDS tag.
        /// </summary>
        /// <param name="values">Initial tag values.</param>
        public MyVuccGridsTag(params string[] values) : base(values) { }
    }
}
