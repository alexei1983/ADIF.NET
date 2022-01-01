
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's transmitter power in watts.
  /// </summary>
  public class RxPwrTag : NumberTag, ITag {

    public override string Name => TagNames.RxPwr;
    }
  }
