
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the logging station's special activity or interest group.
  /// </summary>
  public class MySigIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MySigIntl;

    /// <summary>
    /// Creates a new MY_SIG_INTL tag.
    /// </summary>
    public MySigIntlTag() { }

    /// <summary>
    /// Creates a new MY_SIG_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MySigIntlTag(string value) : base(value) { }
  }
}
