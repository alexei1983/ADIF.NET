
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the logging station's equipment.
  /// </summary>
  public class MyRigTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => ADIFTags.MyRig;

    /// <summary>
    /// Creates a new MY_RIG tag.
    /// </summary>
    public MyRigTag() { }

    /// <summary>
    /// Creates a new MY_RIG tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyRigTag(string value) : base(value) { }
  }
}
