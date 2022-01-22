
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two or four adjacent Maidenhead grid locators for the logging station's grid squares 
  /// that the contacted station may claim for the ARRL VUCC award program.
  /// </summary>
  public class MyVUCCGridsTag : MultiValueStringTag, ITag {

    public override string Name => TagNames.MyVUCCGrids;

    public override string ValueSeparator => Values.COMMA.ToString();

    public override int MaxValueCount => 4;

    public override int MinValueCount => 2;

    public MyVUCCGridsTag() : base() { }

    public MyVUCCGridsTag(string value) : base(value) {
    }
  }
}
