
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QRZQSOUploadStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.QRZQSOUploadStatus;

    public override ADIFEnumeration Options => Values.QSOUploadStatuses;

    public QRZQSOUploadStatusTag() { }

    public QRZQSOUploadStatusTag(string value) : base(value) { }
  }
}
