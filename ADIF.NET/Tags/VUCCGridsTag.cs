
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two or four adjacent Maidenhead grid locators for the contacted station's grid squares 
  /// credited to the QSO for the ARRL VUCC award program.
  /// </summary>
  public class VUCCGridsTag : MultiValueStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.VUCCGrids;

    /// <summary>
    /// String that delimits values in a multi-valued tag.
    /// </summary>
    public override string ValueSeparator => Values.COMMA.ToString();

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
    public VUCCGridsTag() : base() { }

    /// <summary>
    /// Creates a new VUCC_GRIDS tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public VUCCGridsTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new VUCC_GRIDS tag.
    /// </summary>
    /// <param name="values">Initial tag values.</param>
    public VUCCGridsTag(params string[] values) : base(values) { }
  }
}
