using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's longitude.
  /// </summary>
  [DisplayName("The contacted station's longitude.")]
  public class LonTag : StringTag, ITag {

    public override string Name => TagNames.Lon;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && AdifLocation.TryParse(value.ToString(), out _);
      }
    }
  }
