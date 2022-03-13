using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/>.
  /// </summary>
  public class IntlMultilineStringTag : Tag<string>, ITag {

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFIntlMultilineString();

    /// <summary>
    /// Converts the specified object to an ADIF String.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    public override object ConvertValue(object value)
    {
      try
      {
        var result = ADIFIntlMultilineString.Parse(value == null ? string.Empty : value.ToString());
        return result;
      }
      catch (Exception ex)
      {
        throw new ValueConversionException(value, Name, ex);
      }
    }

    /// <summary>
    /// Determines whether or not the specified object can be represented as an ADIF String.
    /// </summary>
    /// <param name="value">Value to validate.</param>
    public override bool ValidateValue(object value)
    {
      try
      {
        ConvertValue(value);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="IntlMultilineStringTag"/> class.
    /// </summary>
    public IntlMultilineStringTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="IntlMultilineStringTag"/> class.
    /// </summary>
    /// <param name="value">Initial value for the tag.</param>
    public IntlMultilineStringTag(string value) : base(value) { }

  }
}
