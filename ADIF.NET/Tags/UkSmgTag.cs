
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
  /// </summary>
  public class UKSMGTag : NumberTag, ITag {

    public override string Name => TagNames.UKSMG;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToDouble() > 0;
      }
    }
  }
