using System;

namespace ADIF.NET.Types {
  public interface IADIFType {

    Type UnderlyingType { get; }

    string Type { get; }

    string FormatString { get; }

    bool IsRange { get; }

    bool IsEnumeration { get; }

    bool RestrictToOptions { get; }

    bool MultiValue { get; }

    IFormatProvider FormatProvider { get; } 

    double MinValue { get; }

    double MaxValue { get; }

    ADIFEnumeration Options { get; }
  }
}
