using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the logging station's longitude.
  /// </summary>
  public class MyLonTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.MyLon;

    /// <summary>
    /// Creates a new MY_LON tag.
    /// </summary>
    public MyLonTag() { }

    /// <summary>
    /// Creates a new MY_LON tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public MyLonTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
