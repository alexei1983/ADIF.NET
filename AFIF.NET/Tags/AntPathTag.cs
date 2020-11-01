using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the signal path.
  /// </summary>
  [DisplayName("The signal path.")]
  public class AntPathTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.AntPath;

    public override string[] Options => typeof(AntennaPath).GetValuesArray();
    }
  }
