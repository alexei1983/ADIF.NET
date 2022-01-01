using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's longitude.
  /// </summary>
  public class LonTag : StringTag, ITag {

    public override string Name => TagNames.Lon;

    public override IADIFType ADIFType => new ADIFLocation();

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
