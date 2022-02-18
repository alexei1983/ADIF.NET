using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/>.
  /// </summary>
  public class StringTag : Tag<string>, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => new ADIFString();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
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
    /// 
    /// </summary>
    /// <param name="value"></param>
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
    /// 
    /// </summary>
    public StringTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public StringTag(string value) : base(value) { }

  }
}
