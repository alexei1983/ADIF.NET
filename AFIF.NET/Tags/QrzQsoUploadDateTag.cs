using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  [DisplayName("")]
  public class QrzQsoUploadDateTag : DateTag, ITag {

    public override string Name => TagNames.QrzQsoUploadDate;

    }
  }
