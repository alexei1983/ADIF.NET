using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class EQSLSentDateTag : DateTag, ITag {

    public override string Name => TagNames.EQSLSentDate;

    public EQSLSentDateTag() { }

    public EQSLSentDateTag(DateTime value) : base(value) { }

  }
}
