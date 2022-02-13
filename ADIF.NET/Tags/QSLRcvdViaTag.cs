
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the means by which the QSL was received by the logging station.
  /// </summary>
  public class QSLRcvdViaTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSLRcvdVia;

    public override ADIFEnumeration Options => Values.Via;

    public QSLRcvdViaTag() { }

    public QSLRcvdViaTag(string value) : base(value) { }
  }
}
