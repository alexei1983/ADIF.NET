
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the logging station's equipment.
  /// </summary>
  public class MyRigTag : StringTag, ITag {

    public override string Name => TagNames.MyRig;

    public MyRigTag() { }

    public MyRigTag(string value) : base(value) { }
  }
}
