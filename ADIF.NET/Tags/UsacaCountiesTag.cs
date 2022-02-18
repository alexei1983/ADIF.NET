
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class USACACountiesTag : MultiValueStringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.USACACounties;

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
    public USACACountiesTag() : base() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public USACACountiesTag(string value) : base(value) {
    }
  }
}
