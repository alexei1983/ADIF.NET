
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the contacted station's equipment.
  /// </summary>
  public class RigIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.RigIntl;

    public RigIntlTag() { }

    public RigIntlTag(string value) : base(value) { }
  }
}
