
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the logging station's equipment.
  /// </summary>
  public class MyRigIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.MyRigIntl;

    public MyRigIntlTag() { }

    public MyRigIntlTag(string value) : base(value) { }
  }
}
