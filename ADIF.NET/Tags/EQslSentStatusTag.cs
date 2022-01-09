using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class EQSLSentStatusTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.EQSLSentStatus;

    [DefaultValue("N")]
    public override ADIFEnumeration Options => Values.EQSLSentStatuses;

    public EQSLSentStatusTag() { }

    public EQSLSentStatusTag(string value) : base(value) { }
  }
}
