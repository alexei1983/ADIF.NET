
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's city.
  /// </summary>
  public class QTHIntlTag : IntlStringTag, ITag {

    public override string Name => TagNames.QTHIntl;

    public QTHIntlTag() { }

    public QTHIntlTag(string value) : base(value) { }
  }
}
