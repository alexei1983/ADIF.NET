
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSO contest identifier.
  /// </summary>
  public class ContestIdTag : EnumerationTag, ITag {

    public override string Name => TagNames.ContestId;

    public override ADIFEnumeration Options => Values.Contests;
    }
  }
