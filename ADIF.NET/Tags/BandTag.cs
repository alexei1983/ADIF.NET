
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the band on which the QSO was made.
  /// </summary>
  public class BandTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.Band;

    public override ADIFEnumeration Options => Values.Bands;
    }
  }
