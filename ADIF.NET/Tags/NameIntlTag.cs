
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.NameIntl;

    public NameIntlTag() { }

    public NameIntlTag(string value) : base(value) { }
  }
}
