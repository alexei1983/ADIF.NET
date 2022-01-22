
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two or four adjacent Maidenhead grid locators for the contacted station's grid squares 
  /// credited to the QSO for the ARRL VUCC award program.
  /// </summary>
  public class VUCCGridsTag : MultiValueStringTag, ITag {

    public override string Name => TagNames.VUCCGrids;

    public override string ValueSeparator => Values.COMMA.ToString();

    public override int MaxValueCount => 4;

    public override int MinValueCount => 2;

    public VUCCGridsTag() : base() { }

    public VUCCGridsTag(string value) : base(value) {
    }
  }
}
