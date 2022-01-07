using System;

namespace ADIF.NET.Tags {
  public class LotwQSLSentDateTag : DateTag, ITag {

    public override string Name => TagNames.LotwQSLSentDate;

    public LotwQSLSentDateTag() { }

    public LotwQSLSentDateTag(DateTime value) : base(value) { }

  }
}
