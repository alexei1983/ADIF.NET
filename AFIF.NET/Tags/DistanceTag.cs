using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the distance between the logging station and the contacted 
  /// station in kilometers via the specified signal path.
  /// </summary>
  [DisplayName("The distance between the logging station and the contacted station in kilometers via the specified signal path.")]
  public class DistanceTag : NumberTag, ITag {

    public override string Name => TagNames.Distance;

    public DistanceTag() { }

    public DistanceTag(double distance) {
      base.SetValue(distance);
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToDouble() > 0;   
      }
    }
  }
