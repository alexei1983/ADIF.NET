
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's name.
  /// </summary>
  public class MyNameIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.MyNameIntl;

    /// <summary>
    /// Creates a new instance of the MY_NAME_INTL tag.
    /// </summary>
    public MyNameIntlTag() { }

    /// <summary>
    /// Creates a new instance of the MY_NAME_INTL tag.
    /// </summary>
    /// <param name="value">Logging operator's name.</param>
    public MyNameIntlTag(string value) : base(value) { }
  }
}
