﻿using ADIF.NET.Exceptions;
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
    /// Creates a new instance of the <see cref="RestrictedEnumerationTag"/> class.
    /// </summary>
    /// <param name="enumValue">Initial tag value.</param>
    public RestrictedEnumerationTag(ADIFEnumerationValue enumValue) : base(enumValue) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is ADIFEnumerationValue enumVal)
      {
        if (!Options.IsValid(enumVal.Code))
          throw new ValueConversionException("Invalid enumeration value.", value.ToString());

        return enumVal.Code;
      }
      else
      {
        return ADIFEnumerationType.Parse(value is string strVal ? strVal : 
                                         value is null ? string.Empty : 
                                         value.ToString(), Options);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      return value is null || (value is string strVal && string.IsNullOrEmpty(strVal)) ||
             ADIFEnumerationType.TryParse(value is string enumStr ? enumStr : 
                                          value.ToString(), Options, out _);
    }
  }
}
