
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contest QSO transmitted serial number.
  /// </summary>
  public class StxStringTag : StringTag, ITag {

    public override string Name => TagNames.StxString;
    }
  }
