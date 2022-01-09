
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents the contacted station's email address.
  /// </summary>
  public class EmailTag : StringTag, ITag {

    public override string Name => TagNames.Email;

    public EmailTag() { }

    public EmailTag(string value) : base(value) { }

    public override bool ValidateValue(object value)
    {
      return base.ValidateValue(value) && value.ToString().IsEmailAddress();
    }

  }
}
