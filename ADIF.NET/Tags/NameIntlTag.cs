
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's operator's name.
  /// </summary>
  public class NameIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.NameIntl;

    /// <summary>
    /// Creates a new instance of the NAME_INTL tag.
    /// </summary>
    public NameIntlTag() { }

    /// <summary>
    /// Creates a new instance of the NAME_INTL tag.
    /// </summary>
    /// <param name="value">Contacted station's operator's name.</param>
    public NameIntlTag(string value) : base(value) { }
  }
}
