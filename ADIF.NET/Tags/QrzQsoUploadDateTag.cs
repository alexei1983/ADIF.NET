using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class QRZQSOUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.QRZQSOUploadDate;

    public QRZQSOUploadDateTag() { }

    public QRZQSOUploadDateTag(DateTime value) : base(value) { }

    }
  }
