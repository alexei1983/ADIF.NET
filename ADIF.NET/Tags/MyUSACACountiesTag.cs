
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class MyUSACACountiesTag : MultiValueStringTag, ITag {

    public override string Name => TagNames.MyUSACACounties;

    public override string ValueSeparator => Values.COLON.ToString();

    public override int MaxValueCount => 2;

    public override int MinValueCount => 2;

    public MyUSACACountiesTag() : base() { }

    public MyUSACACountiesTag(string value) : base(value) {
    }
  }
}
