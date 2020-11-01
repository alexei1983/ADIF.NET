using System;

namespace ADIF.NET.Tags {

  public interface OldITag : IFormattable {

    string Name { get; }

    string TextValue { get; }

    object[] Value { get; }

    string FormatString { get; set; }

    string ValueSeparator { get; set; }

    IFormatProvider FormatProvider { get; set; }

    Type ExpectedValueType { get; }

    int? ValueLength { get; }

    int ValueCount { get; }

    string[] Options { get; }

    bool RestrictOptions { get; }

    bool SuppressLength { get; }

    bool Header { get; }

    void AddValue(object value);

    void AddValues(params object[] values);

    void ClearValues();

    bool ValidateValue(object value);
    }
  }
