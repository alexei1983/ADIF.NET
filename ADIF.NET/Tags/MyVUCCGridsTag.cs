
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two or four adjacent Maidenhead grid locators for the logging station's grid squares 
  /// that the contacted station may claim for the ARRL VUCC award program.
  /// </summary>
  public class MyVUCCGridsTag : MultiValueStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyVUCCGrids;

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
    public MyVUCCGridsTag() : base() { }

    /// <summary>
    /// Creates a new MY_VUCC_GRIDS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyVUCCGridsTag(string value) : base(value) {
    }

    /// <summary>
    /// Creates a new MY_VUCC_GRIDS tag.
    /// </summary>
    /// <param name="values">Initial tag values.</param>
    public MyVUCCGridsTag(params string[] values) : base(values) { }
  }
}
