
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QRZQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadStatus;

    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    public QRZQSOUploadStatusTag() { }

    public QRZQSOUploadStatusTag(string value) : base(value) { }
  }
}
