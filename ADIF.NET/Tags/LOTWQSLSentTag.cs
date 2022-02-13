
namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LOTWQSLSentTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.LOTWQSLSentStatus;

    public override ADIFEnumeration Options => Values.QSLSentStatuses;

    public LOTWQSLSentTag() { }

    public LOTWQSLSentTag(string value) : base(value) { }
  }
}
