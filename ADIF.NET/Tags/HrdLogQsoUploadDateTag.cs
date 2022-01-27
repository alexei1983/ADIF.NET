using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class HRDLogQSOUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.HrdLogQSOUploadDate;

    public HRDLogQSOUploadDateTag() { }

    public HRDLogQSOUploadDateTag(DateTime value) : base(value) { }

  }
  }
