
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL sent status.
  /// </summary>
  public class QSLSentTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSLSent;

    public override ADIFEnumeration Options => Values.QSLReceivedStatuses;

    public QSLSentTag() { }

    public QSLSentTag(string value) : base(value) { }
  }
}
