using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/>.
  /// </summary>
  public class StringTag : Tag<string>, ITag {

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFString();

    /// <summary>
    /// Converts the specified object to an ADIF String.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    public override object ConvertValue(object value)
    {
      try
      {
        var result = ADIFString.Parse(value == null ? string.Empty : value.ToString());
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
    /// Creates a new instance of the <see cref="StringTag"/> class.
    /// </summary>
    public StringTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="StringTag"/> class.
    /// </summary>
    /// <param name="value">Initial value for the tag.</param>
    public StringTag(string value) : base(value) { }

  }
}
