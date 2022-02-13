
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the means by which the QSL was sent by the logging station.
  /// </summary>
  public class QSLSentViaTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSLSentVia;

    public override ADIFEnumeration Options => Values.Via;

    public QSLSentViaTag() { }

    public QSLSentViaTag(string value) : base(value) { }
  }
}
