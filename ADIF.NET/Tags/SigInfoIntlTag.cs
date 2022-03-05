
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents information associated with the contacted station's activity or interest group.
  /// </summary>
  public class SigInfoIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.SigInfoIntl;

    /// <summary>
    /// Creates a new SIG_INFO_INTL tag.
    /// </summary>
    public SigInfoIntlTag() { }

    /// <summary>
    /// Creates a new SIG_INFO_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public SigInfoIntlTag(string value) : base(value) { }
  }
}
