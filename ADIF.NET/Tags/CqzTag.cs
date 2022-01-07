
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's CQ zone.
  /// </summary>
  public class CQZTag : NumberTag, ITag {

    public override string Name => TagNames.CQZ;

    public CQZTag() { }

    public CQZTag(double value) : base(value) { }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value);         
      }
    }
  }
