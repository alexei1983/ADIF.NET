
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's transmitter power in watts.
  /// </summary>
  public class RxPwrTag : NumberTag, ITag {

    public override string Name => TagNames.RxPwr;

    public override double MinValue => 0;

    public override double MaxValue => double.MaxValue;

    public RxPwrTag() { }

    public RxPwrTag(double value) : base(value) { }
  }
}
