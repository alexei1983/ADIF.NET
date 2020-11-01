using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's transmitter power in watts.
  /// </summary>
  [DisplayName("The contacted station's transmitter power in watts.")]
  public class RxPwrTag : NumberTag, ITag {

    public override string Name => TagNames.RxPwr;
    }
  }
