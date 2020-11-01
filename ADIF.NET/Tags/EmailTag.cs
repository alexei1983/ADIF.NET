using ADIF.NET.Attributes;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's email address.
  /// </summary>
  [DisplayName("The contacted station's email address.")]
  public class EmailTag : StringTag, ITag {

    public override string Name => TagNames.Email;

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && value.ToString().IsEmailAddress();
      }

    }
  }
