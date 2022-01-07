using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class LotwQSLReceivedDateTag : DateTag, ITag {

    public override string Name => TagNames.LotwQSLReceivedDate;

    public LotwQSLReceivedDateTag() { }

    public LotwQSLReceivedDateTag(DateTime value) : base(value) { }

  }
}
