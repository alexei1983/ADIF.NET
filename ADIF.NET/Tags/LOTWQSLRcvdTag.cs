
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LOTWQSLRcvdTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.LOTWQSLRcvdStatus;

    public override ADIFEnumeration Options => Values.QSLReceivedStatuses;

    public LOTWQSLRcvdTag() { }

    public LOTWQSLRcvdTag(string value) : base(value) { }
  }
}
