using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the band on which the QSO was made.
  /// </summary>
  [DisplayName("The band on which the QSO was made.")]
  public class BandTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Band;

    public override string[] Options => typeof(Band).GetValuesArray();
    }
  }
