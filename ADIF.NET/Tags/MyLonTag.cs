using ADIF.NET.Attributes;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's longitude.
  /// </summary>
  [DisplayName("The logging station's longitude.")]
  public class MyLonTag : StringTag, ITag {

    public override string Name => TagNames.MyLon;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
