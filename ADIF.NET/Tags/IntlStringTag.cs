using System;
using ADIF.NET.Exceptions;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is of type <see cref="string"/> 
  /// with the potential presence of non-ASCII characters.
  /// </summary>
  public class IntlStringTag : Tag<string>, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override IADIFType ADIFType => new ADIFIntlString();

    /// <summary>
    /// 
    /// </summary>
    public IntlStringTag() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public IntlStringTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      try
      {
        var strVal = value is string ? (string)value : value != null ? value.ToString() : string.Empty;
        return ADIFIntlString.Parse(strVal);
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
  }
}
