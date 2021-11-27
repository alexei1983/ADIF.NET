using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the propagation mode for the QSO.
  /// </summary>
  [DisplayName("The QSO propagation mode.")]
  public class PropModeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.PropMode;

    public override string[] Options => Values.PropagationModes.GetOptions();
    }
  }
