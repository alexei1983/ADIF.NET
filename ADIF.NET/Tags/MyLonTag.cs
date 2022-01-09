using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's longitude.
  /// </summary>
  public class MyLonTag : StringTag, ITag {

    public override string Name => TagNames.MyLon;

    public MyLonTag() { }

    public MyLonTag(string value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
