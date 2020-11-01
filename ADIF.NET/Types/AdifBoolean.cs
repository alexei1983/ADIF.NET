using System;

namespace ADIF.NET.Types {
  public class AdifBoolean : AdifType {

    public override string[] Options => typeof(BooleanValue).GetValuesArray();
    public override bool IsEnumeration => true;
    public override bool RestrictToOptions => true;
    public override Type UnderlyingType => typeof(bool?);

    public static bool? Parse(object value) {

      if (value is bool || value is bool?)
        return (bool?)value;

      return value?.ToString()?.ToNullableBooleanAdif();
      }

    public static bool TryParse(string str, out bool? value) {

      value = null;
      str = (str ?? string.Empty).ToUpper();
      var options = typeof(BooleanValue).GetValuesArray();

      if (Array.IndexOf(options, str) >= 0) {
        value = str.ToAdifBoolean();
        return true;
        }
      
      return false;
      }
  
    public override bool IsValidValue(object value) {
      
      if (value is bool || value is bool?)
        return true;

      var valStr = value?.ToString()?.ToUpper() ?? string.Empty;

      return valStr.Equals("Y") || valStr.Equals("N") || valStr.Equals(string.Empty);
      }
    }
  }
