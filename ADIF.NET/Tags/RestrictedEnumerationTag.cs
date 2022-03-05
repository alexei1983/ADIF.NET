using ADIF.NET.Exceptions;
using ADIF.NET.Types;

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

    /// <summary>
    /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
    /// </summary>
    public RestrictedEnumerationTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public RestrictedEnumerationTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      var enumVal = string.Empty;

      if (value == null)
        value = string.Empty;

      if (value is ADIFEnumerationValue enumValObj)
        enumVal = enumValObj.Code;
      else if (value is string strVal)
        enumVal = strVal;
      else
      {
        if (!ADIFString.TryParse(value.ToString(), out enumVal))
          throw new ValueConversionException("Invalid enumeration value.", value.ToString());
      }

      if (!Options.IsValid(enumVal))
        throw new InvalidEnumerationOptionException($"'{enumVal}' is not a valid enumeration option in tag {Name}.", enumVal);

      return enumVal;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      return base.ValidateValue(value) &&
             Options.IsValid(value.ToString());
    }
  }
}
