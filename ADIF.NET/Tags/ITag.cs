using System;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Defines the properties and methods that all ADIF.NET tags must implement.
  /// </summary>
  public interface ITag : IFormattable {

    string Name { get; }

    string TextValue { get; }

    object Value { get; }

    string FormatString { get; set; }

    string ValueSeparator { get; set; }

    IFormatProvider FormatProvider { get; set; }

    Type ExpectedValueType { get; }

    int? ValueLength { get; }

    IADIFType ADIFType { get; }

    ADIFEnumeration Options { get; }

    bool RestrictOptions { get; }

    bool SuppressLength { get; }

    bool Header { get; }

    void SetValue(object value);

    void ClearValue();

    bool ValidateValue(object value);

    bool ValidateValue();

    object ConvertValue(object value);

    string ToString(string format);

    }
  }