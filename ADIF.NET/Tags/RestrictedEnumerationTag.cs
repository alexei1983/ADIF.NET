
namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value must be selected from a list 
  /// of valid options.
  /// </summary>
  public class RestrictedEnumerationTag : EnumerationTag, ITag {

    /// <summary>
    /// Whether or not to restrict the tag value to the specified enumeration options.
    /// </summary>
    public override bool RestrictOptions => true;

    public RestrictedEnumerationTag() { }

    public RestrictedEnumerationTag(string value) : base(value) { }

    public override bool ValidateValue(object value)
    {
      return base.ValidateValue(value) &&
             this.Options.IsValid(value.ToString());
    }
  }
}
