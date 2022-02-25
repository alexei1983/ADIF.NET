using System;

namespace ADIF.NET.Types {
  public interface IADIFType {

    Type UnderlyingType { get; }

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    string Type { get; }

    string FormatString { get; }

    bool IsRange { get; }

    bool IsEnumeration { get; }

    bool MultiValue { get; }

    IFormatProvider FormatProvider { get; } 

    double MinValue { get; }

    double MaxValue { get; }
  }
}
