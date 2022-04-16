using System;
using System.Xml;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Defines the properties and methods that all ADIF.NET tags must implement.
  /// </summary>
  public interface ITag : IFormattable, ICloneable {

    /// <summary>
    /// Tag name.
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

    /// <summary>
    /// String that determines how the tag's value will be formatted.
    /// </summary>
    string FormatString { get; set; }

    /// <summary>
    /// String that separates values in a multivalued tag.
    /// </summary>
    string ValueSeparator { get; set; }

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    string DataType { get; }

    IFormatProvider FormatProvider { get; set; }

    /// <summary>
    /// The underlying <see cref="Type"/> of the tag's value.
    /// </summary>
    Type ExpectedValueType { get; }

    /// <summary>
    /// Length of the text value of the tag.
    /// </summary>
    int? ValueLength { get; }

    /// <summary>
    /// ADIF type.
    /// </summary>
    IADIFType ADIFType { get; }

    /// <summary>
    /// 
    /// </summary>
    ADIFEnumeration Options { get; }

    /// <summary>
    /// 
    /// </summary>
    bool RestrictOptions { get; }

    /// <summary>
    /// 
    /// </summary>
    bool SuppressLength { get; }

    /// <summary>
    /// Whether or not the tag is user-defined.
    /// </summary>
    bool IsUserDef { get; }

    /// <summary>
    /// Whether or not the tag is application-defined.
    /// </summary>
    bool IsAppDef { get; }

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    bool Header { get; }

    /// <summary>
    /// Sets the value of the tag.
    /// </summary>
    /// <param name="value">Tag value to set.</param>
    void SetValue(object value);

    /// <summary>
    /// Clears the value of the tag.
    /// </summary>
    void ClearValue();

    /// <summary>
    /// Determines whether or not the specified value is valid for the tag.
    /// </summary>
    /// <param name="value">Value to validate.</param>
    bool ValidateValue(object value);

    /// <summary>
    /// Determines whether or not the current tag value is valid.
    /// </summary>
    bool ValidateValue();

    /// <summary>
    /// Retrieves the current value of the tag.
    /// </summary>
    object GetValue();

    /// <summary>
    /// Determines whether or not the tag has a value.
    /// </summary>
    bool HasValue();

    /// <summary>
    /// Converts the specified value to the appropriate type for the tag.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    object ConvertValue(object value);

    /// <summary>
    /// Returns a string representation of the tag.
    /// </summary>
    /// <param name="format">String format.</param>
    string ToString(string format);

    /// <summary>
    /// Returns the tag represented as an instance of the <see cref="XmlElement"/> class.
    /// </summary>
    /// <param name="document"><see cref="XmlDocument"/> object to which the tag will be appended.</param>
    XmlElement ToXml(XmlDocument document);

    }
  }