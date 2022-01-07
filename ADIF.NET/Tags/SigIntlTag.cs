
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the contacted station's special activity or interest group.
  /// </summary>
  public class SigIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.SigIntl;

    public SigIntlTag() { }

    public SigIntlTag(string value) : base(value) { }
  }
}
