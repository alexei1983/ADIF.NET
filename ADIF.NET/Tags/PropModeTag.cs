
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the propagation mode for the QSO.
  /// </summary>
  public class PropModeTag : RestrictedEnumerationTag, ITag {

    public override string Name => TagNames.PropMode;

    public override ADIFEnumeration Options => Values.PropagationModes;
    }
  }
