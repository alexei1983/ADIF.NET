
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL received status.
  /// </summary>
  public class QSLRcvdTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSLRcvd;

    public override ADIFEnumeration Options => Values.QSLReceivedStatuses;

    public QSLRcvdTag() { }

    public QSLRcvdTag(string value) : base(value) { }
  }
}
