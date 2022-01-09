using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class EQSLReceivedStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQSLReceivedStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.EQSLReceivedStatuses;

    public EQSLReceivedStatusTag() { }

    public EQSLReceivedStatusTag(string value) : base(value) { }
  }
}
