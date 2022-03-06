
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's city.
  /// </summary>
  public class QTHIntlTag : IntlStringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.QTHIntl;

    /// <summary>
    /// Creates a new QTH_INTL tag.
    /// </summary>
    public QTHIntlTag() { }

    /// <summary>
    /// Creates a new QTH_INTL tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public QTHIntlTag(string value) : base(value) { }
  }
}
