using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the name of the meteor shower in a meteor scatter QSO.
  /// </summary>
  [DisplayName("For meteor scatter QSOs, the name of the meteor shower in progress.")]
  public class MsShowerTag : StringTag, ITag {

    public override string Name => TagNames.MsShower;
    }
  }
