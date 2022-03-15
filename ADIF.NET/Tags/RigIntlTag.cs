
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the description of the contacted station's equipment.
  /// </summary>
  public class RigIntlTag : IntlMultilineStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.RigIntl;

    /// <summary>
    /// Creates a new RIG_INTL tag.
    /// </summary>
    public RigIntlTag() { }

    /// <summary>
    /// Creates a new RIG_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public RigIntlTag(string value) : base(value) { }
  }
}
