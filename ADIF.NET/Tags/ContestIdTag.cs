using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSO contest identifier.
  /// </summary>
  [DisplayName("The QSO contest identifier.")]
  public class ContestIdTag : EnumerationTag, ITag {

    public override string Name => TagNames.ContestId;

    public override string[] Options => null;
    }
  }
