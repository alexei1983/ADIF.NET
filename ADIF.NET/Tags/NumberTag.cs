using System;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is numeric as represented by 
  /// a value of type <see cref="double?"/>.
  /// </summary>
  public class NumberTag : Tag<double?>, ITag {

    public virtual double MinValue { get; }

    public virtual double MaxValue { get; }

    public override IADIFType ADIFType => new ADIFNumber();

    public virtual bool AllowValuesOverMaxOnImport { get; }

    public override object ConvertValue(object value)
    {
      if (value is double || value is double?)
        return (double?)value;
      else if (ADIFNumber.TryParse(value == null ? string.Empty : value.ToString(), out double? result))
        return result;

      return null;
    }

    public override bool ValidateValue(object value)
    {
      return base.ValidateValue(value) && ConvertValue(value) is double?;
    }
  }
}
