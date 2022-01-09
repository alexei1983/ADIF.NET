using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class EQSLReceivedDateTag : DateTag, ITag {

    public override string Name => TagNames.EQSLReceivedDate;

    public EQSLReceivedDateTag() { }

    public EQSLReceivedDateTag(DateTime value) : base(value) { }

  }
}
