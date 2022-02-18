
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's name.
  /// </summary>
  public class MyNameTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.MyName;

    /// <summary>
    /// Creates a new instance of the MY_NAME tag.
    /// </summary>
    public MyNameTag() { }

    /// <summary>
    /// Creates a new instance of the MY_NAME tag.
    /// </summary>
    /// <param name="value">Logging operator's name.</param>
    public MyNameTag(string value) : base(value) { }
  }
}
