using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class HrdLogQSOUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadDate;

    public HrdLogQSOUploadDateTag() { }

    public HrdLogQSOUploadDateTag(DateTime value) : base(value) { }

  }
  }
