using ADIF.NET.Attributes;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's latitude.
  /// </summary>
  [DisplayName("The logging station's latitude.")]
  public class MyLatTag : StringTag, ITag {

    public override string Name => TagNames.MyLat;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
