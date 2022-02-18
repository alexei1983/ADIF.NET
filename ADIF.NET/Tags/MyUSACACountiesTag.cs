
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class MyUSACACountiesTag : MultiValueStringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.MyUSACACounties;

    /// <summary>
    /// 
    /// </summary>
    public override string ValueSeparator => Values.COLON.ToString();

    /// <summary>
    /// 
    /// </summary>
    public override int MaxValueCount => 2;

    /// <summary>
    /// 
    /// </summary>
    public override int MinValueCount => 2;

    /// <summary>
    /// 
    /// </summary>
    public MyUSACACountiesTag() : base() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public MyUSACACountiesTag(string value) : base(value) {
    }
  }
}
