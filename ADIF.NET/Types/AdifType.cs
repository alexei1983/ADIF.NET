using System;
using System.Globalization;

namespace ADIF.NET.Types {
  public class AdifType {

    public virtual Type UnderlyingType { get; }

    public virtual string FormatString { get; }

    public virtual bool IsRange { get; }

    public virtual bool IsEnumeration { get; }

    public virtual bool RestrictToOptions { get; }

    public virtual bool MultiValue { get; }

    public virtual IFormatProvider FormatProvider { get; } = CultureInfo.CurrentCulture;

    public virtual double MinValue { get; } 

    public virtual double MaxValue { get; }

    public virtual string[] Options { get; }

    public virtual bool IsValidValue(object value) {
      return !(value is null); 
      }

    public virtual bool IsValidValue(string value) {
      return !string.IsNullOrEmpty(value);
      }
    }
  }
