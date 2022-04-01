using System;
using System.Text.RegularExpressions;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// 
  /// </summary>
  public class BaseCallSignTag : StringTag, ITag, IFormattable, ICloneable {

    /// <summary>
    /// Creates a new instance of the <see cref="BaseCallSignTag"/> class.
    /// </summary>
    public BaseCallSignTag() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="BaseCallSignTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public BaseCallSignTag(string value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (base.ConvertValue(value) is string strCall)
        return strCall.ToUpper();

      throw new ValueConversionException(value, Name);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      if (value is null)
        return true;

      var strVal = value is string strCall ? strCall : value.ToString();

      if (string.IsNullOrEmpty(strVal))
        return true;

      return Regex.IsMatch(strVal, Values.CALLSIGN_REGEX);
    }
  }
}
