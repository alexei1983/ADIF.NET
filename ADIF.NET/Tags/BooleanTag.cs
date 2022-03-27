using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the value is either <see cref="true"/> or <see cref="false"/>.
  /// </summary>
  public class BooleanTag : Tag<bool?>, ITag {

    /// <summary>
    /// Value of the tag as a <see cref="string"/>.
    /// </summary>
    public override string TextValue
    {
      get
      {
        return Value.HasValue && Value.Value ? Values.ADIF_BOOLEAN_TRUE :
               Value.HasValue && !Value.Value ? Values.ADIF_BOOLEAN_FALSE :
               string.Empty;
      }
    }

    /// <summary>
    /// Valid enumeration values.
    /// </summary>
    public override ADIFEnumeration Options => Values.BooleanValues;

    /// <summary>
    /// ADIF type.
    /// </summary>
    public override IADIFType ADIFType => new ADIFBoolean();

    /// <summary>
    /// Creates a new instance of the <see cref="BooleanTag"/> class.
    /// </summary>
    public BooleanTag() { }

    /// <summary>
    /// Creates a new instance of the <see cref="BooleanTag"/> class.
    /// </summary>
    /// <param name="value">Initial tag value.</param>
    public BooleanTag(bool value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value)
    {
      if (value is bool boolVal)
        return boolVal;
      else if (value is bool?)
        return (bool?)value;
      else
      {
        try
        {
          return ADIFBoolean.Parse(value is null ? string.Empty : value.ToString());
        }
        catch (Exception ex)
        {
          throw new ValueConversionException(value, Name, ex);
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override bool ValidateValue(object value)
    {
      return value is null || (value is string strVal && string.IsNullOrEmpty(strVal)) ||
             value is bool || value is bool? || ADIFBoolean.TryParse(value.ToString(), out _);
    }
  }
}
