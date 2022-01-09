
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's city.
  /// </summary>
  public class MyCityIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyCityIntl;

    public MyCityIntlTag() { }

    public MyCityIntlTag(string value) : base(value) { }
  }
}
