using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's latitude.
  /// </summary>
  [DisplayName("The contacted station's latitude.")]
  public class LatTag : StringTag, ITag {

    public override string Name => TagNames.Lat;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && AdifLocation.TryParse(value.ToString(), out _);
      }
    }
  }
