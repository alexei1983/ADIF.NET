
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents two US counties where the contacted station is located on a border between two counties.
  /// </summary>
  public class MyUSACACountiesTag : MultiValueStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyUSACACounties;

    /// <summary>
    /// 
    /// </summary>
    public override string ValueSeparator => Values.COLON.ToString();

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
    public MyUSACACountiesTag() : base() { }

    /// <summary>
    /// Creates a new MY_USACA_COUNTIES tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyUSACACountiesTag(string value) : base(value) { }

    /// <summary>
    /// Creates a new MY_USACA_COUNTIES tag.
    /// </summary>
    /// <param name="values">Initial tag values.</param>
    public MyUSACACountiesTag(params string[] values) : base(values) { }
  }
}
