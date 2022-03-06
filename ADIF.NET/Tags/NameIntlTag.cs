
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.NameIntl;

    /// <summary>
    /// Creates a new NAME_INTL tag.
    /// </summary>
    public NameIntlTag() { }

    /// <summary>
    /// Creates a new NAME_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public NameIntlTag(string value) : base(value) { }
  }
}
