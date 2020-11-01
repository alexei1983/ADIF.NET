using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the solar flux at the time of the QSO.
  /// </summary>
  [DisplayName("The solar flux at the time of the QSO.")]
  public class SfiTag : NumberTag, ITag {

    public override string Name => TagNames.Sfi;

    public override double MaxValue => 300d;

    public override double MinValue => 0d;

    }
  }
