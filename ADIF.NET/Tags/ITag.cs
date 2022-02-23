using System;
using System.Xml;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Defines the properties and methods that all ADIF.NET tags must implement.
  /// </summary>
  public interface ITag : IFormattable, ICloneable {

    /// <summary>
    /// Name of the tag.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Value of the tag as a <see cref="string"/>.
    /// </summary>
    string TextValue { get; }

    /// <summary>
    /// Value of the tag as an <see cref="object"/>.
    /// </summary>
    object Value { get; }

    string FormatString { get; set; }

    string ValueSeparator { get; set; }

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    string DataType { get; }

    IFormatProvider FormatProvider { get; set; }

    Type ExpectedValueType { get; }

    /// <summary>
    /// Length of the text value of the tag.
    /// </summary>
    int? ValueLength { get; }

    /// <summary>
    /// ADIF type.
    /// </summary>
    IADIFType ADIFType { get; }

    ADIFEnumeration Options { get; }

    bool RestrictOptions { get; }

    bool SuppressLength { get; }

    /// <summary>
    /// Whether or not the tag is user-defined.
    /// </summary>
    bool IsUserDef { get; }

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    bool Header { get; }

    /// <summary>
    /// Sets the value of the tag.
    /// </summary>
    /// <param name="value">Tag value to set.</param>
    void SetValue(object value);

    void ClearValue();

    bool ValidateValue(object value);

    bool ValidateValue();

    object GetValue();

    /// <summary>
    /// Determines whether or not the current tag has a value.
    /// </summary>
    bool HasValue();

    object ConvertValue(object value);

    string ToString(string format);

    XmlElement ToXml(XmlDocument document);

    }
  }