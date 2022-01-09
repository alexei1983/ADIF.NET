using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's latitude.
  /// </summary>
  public class MyLatTag : StringTag, ITag {

    public override string Name => TagNames.MyLat;

    public MyLatTag() { }

    public MyLatTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
