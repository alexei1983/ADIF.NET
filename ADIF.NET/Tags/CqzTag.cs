using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's CQ zone.
  /// </summary>
  [DisplayName("The contacted station's CQ Zone.")]
  public class CqzTag : NumberTag, ITag {

    public override string Name => TagNames.Cqz;

    public CqzTag() { }

    public CqzTag(double zone) {
      base.SetValue(zone);
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }
    }
  }
