using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's longitude.
  /// </summary>
  public class LonTag : StringTag, ITag {

    /// <summary>
    /// Tag name.
    /// </summary>
    public override string Name => TagNames.Lon;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFLocation();

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    public LonTag() { }

    /// <summary>
    /// Creates a new LON tag.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public LonTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ADIFLocation.TryParse(value.ToString(), out _);
      }
    }
  }
