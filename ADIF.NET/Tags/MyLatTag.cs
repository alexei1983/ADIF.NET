using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's latitude.
  /// </summary>
  public class MyLatTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyLat;

    /// <summary>
    /// Creates a new MY_LAT tag.
    /// </summary>
    public MyLatTag() { }

    /// <summary>
    /// Creates a new MY_LAT tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyLatTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
