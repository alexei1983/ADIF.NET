using System;
using ADIF.NET.Types;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the value is either <see cref="true"/> or <see cref="false"/>.
  /// </summary>
  public class BooleanTag : Tag<bool?>, ITag {

    /// <summary>
    /// 
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

    public override ADIFEnumeration Options => Values.BooleanValues;

    public override IADIFType ADIFType => new ADIFBoolean();

    public BooleanTag() { }

    public BooleanTag(bool value) : base(value) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override object ConvertValue(object value) {
      if (value is bool boolVal)
        return boolVal;
      else if (value is bool?)
        return (bool?)value;
      else
      {
        try
        {
          return ADIFBoolean.Parse(value == null ? string.Empty : value.ToString());
        }
        catch (Exception ex)
        {
          throw new ValueConversionException(value, Name, ex);
        }
      }
      }
    }
  }
