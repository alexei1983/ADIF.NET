
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging operator's name.
  /// </summary>
  public class MyNameIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyNameIntl;

    public MyNameIntlTag() { }

    public MyNameIntlTag(string value) : base(value) { }
  }
}
