using System;

namespace ADIF.NET.Types {
  public class AdifNumber : AdifType {

    public override string[] Options => typeof(BooleanValue).GetValuesArray();
    public override Type UnderlyingType => typeof(double?);
    public override double MinValue => double.MinValue;
    public override double MaxValue => double.MaxValue;

    public static double? Parse(object value) {

      if (value is double || value is double?)
        return (double?)value;

      var val = default(double?);

      try {
        val = Convert.ToDouble(value);
        return val;
        }
      catch (Exception ex) {
        throw new InvalidOperationException($"Could not convert value to {nameof(AdifNumber)}", ex);
        }
      }
  
    public override bool IsValidValue(object value) {
      
      if (value is double || value is double?)
        return true;

      var valStr = value?.ToString()?.ToUpper() ?? string.Empty;

      return valStr.Equals("Y") || valStr.Equals("N") || valStr.Equals(string.Empty);
      }
    }
  }
