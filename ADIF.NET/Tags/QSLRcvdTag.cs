
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSL received status.
  /// </summary>
  public class QSLRcvdTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QSLRcvd;

    // TODO: Fix enumeration here
    public override ADIFEnumeration Options => Values.EQSLReceivedStatuses;

    public QSLRcvdTag() { }

    public QSLRcvdTag(string value) : base(value) { }
  }
}
