
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the maximum length of meteor scatter bursts heard by the logging station, in seconds.
  /// </summary>
  public class MaxBurstsTag : NumberTag, ITag {

    public override string Name => TagNames.MaxBursts;

    public MaxBurstsTag() { }

    public MaxBurstsTag(double value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToDouble() >= 0;
      }
    }
  }
