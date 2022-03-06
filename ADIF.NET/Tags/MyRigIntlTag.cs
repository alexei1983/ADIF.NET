
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the logging station's equipment.
  /// </summary>
  public class MyRigIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyRigIntl;

    /// <summary>
    /// Creates a new MY_RIG_INTL tag.
    /// </summary>
    public MyRigIntlTag() { }

    /// <summary>
    /// Creates a new MY_RIG_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyRigIntlTag(string value) : base(value) { }
  }
}
