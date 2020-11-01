using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's UKSMG (UK Six Metre Group) member number.
  /// </summary>
  [DisplayName("The contacted station's UKSMG member number.")]
  public class UkSmgTag : NumberTag, ITag {

    public override string Name => TagNames.UkSmg;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToDouble() > 0;
      }
    }
  }
