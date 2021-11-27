using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class QRZQSOUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.QrzQSOUploadDate;

    }
  }
