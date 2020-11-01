using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the QSO frequency in Megahertz.
  /// </summary>
  [DisplayName("The QSO frequency in Megahertz.")]
  public class FreqTag : NumberTag, ITag {

    public override string Name => TagNames.Freq;
    }
  }
