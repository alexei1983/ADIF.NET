using System;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an ADIF.NET tag where the underlying value is numeric as represented by 
  /// a value of type <see cref="double"/>.
  /// </summary>
  public class NumberTag : Tag<double>, ITag {

    public virtual double MinValue { get; }

    public virtual double MaxValue { get; }

    public override IADIFType ADIFType => new ADIFNumber();

    public virtual bool AllowValuesOverMaxOnImport { get; }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        if (value is double doubleVal)
          return doubleVal;
        else if (value.IsNumber()) {
          try {
            var convertedDblVal = Convert.ToDouble(value);
            return convertedDblVal;
            }
          catch {
            }
          }
        else if (double.TryParse(value.ToString(), out double parsedDblVal))
          return parsedDblVal;
        }

      return null;
      }

    public override bool ValidateValue(object value) {
      return base.ValidateValue(value) && ConvertValue(value) is double;
      }
    }
  }
