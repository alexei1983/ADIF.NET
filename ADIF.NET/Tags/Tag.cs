using System;
using System.Globalization;
using System.Xml;
using ADIF.NET.Types;

namespace ADIF.NET.Tags {

  /// <summary>
  /// The base class from which all ADIF.NET tags inherit.
  /// </summary>
  /// <typeparam name="T">The <see cref="Type"/> of the underlying value of the tag.</typeparam>
  public class Tag<T> : ITag, IFormattable, IEquatable<Tag<T>>, ICloneable {

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// The text value of the tag.
    /// </summary>
    public virtual string TextValue
    {
      get
      {
        var textValue = string.Empty;

        var val = Value;

        if (val is IFormattable formattable)
          textValue = formattable.ToString(FormatString, FormatProvider ?? CultureInfo.CurrentCulture) ?? string.Empty;
        else
          textValue = val?.ToString() ?? string.Empty;

        return textValue;
      }
    }

    /// <summary>
    /// Whether or not the tag is a header tag.
    /// </summary>
    public virtual bool Header { get; }

    /// <summary>
    /// ADIF type.
    /// </summary>
    public virtual IADIFType ADIFType { get; }

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public virtual string DataType
    {
      get
      {
        return ADIFType != null ? ADIFType.Type : string.Empty;
      }
    }

    /// <summary>
    /// String that delimits values in a multivalued ADIF tag.
    /// </summary>
    public virtual string ValueSeparator { get; set; }

    /// <summary>
    /// The value of the tag as an <see cref="object"/>.
    /// </summary>
    object ITag.Value { get { return this.Value; } }

    /// <summary>
    /// The strongly-typed value of the tag as type <typeparamref name="T"/>.
    /// </summary>
    public virtual T Value { get; private set; }

    /// <summary>
    /// The string used to format the text value of the tag.
    /// </summary>
    public virtual string FormatString { get; set; }

    /// <summary>
    /// The culture-specific <see cref="IFormatProvider"/> object used to 
    /// format the text value of the tag.
    /// </summary>
    public virtual IFormatProvider FormatProvider { get; set; } = CultureInfo.CurrentCulture;

    /// <summary>
    /// The <see cref="Type"/> of the underlying value of the tag.
    /// </summary>
    public virtual Type ExpectedValueType => typeof(T);

    /// <summary>
    /// The number of characters in the text value.
    /// </summary>
    public virtual int? ValueLength
    {
      get
      {
        if (!SuppressLength)
          return TextValue.Length;
        else
          return null;
      }
    }

    /// <summary>
    /// The valid values for an enumeration-type tag.
    /// </summary>
    public virtual ADIFEnumeration Options { get; }

    /// <summary>
    /// Whether or not the value of the tag is restricted solely to the options.
    /// </summary>
    public virtual bool RestrictOptions { get; }

    /// <summary>
    /// Whether or not the length of the tag's value will be suppressed when 
    /// emitting the tag as output.
    /// </summary>
    public virtual bool SuppressLength { get; }

    /// <summary>
    /// Whether or not the tag is a user-defined tag.
    /// </summary>
    public virtual bool IsUserDef { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Tag{T}"/> class.
    /// </summary>
    public Tag()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Tag{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the tag as type <typeparamref name="T"/>.</param>
    public Tag(T value)
    {
      SetValue(value);
    }

    /// <summary>
    /// Sets the value of the tag.
    /// </summary>
    /// <param name="value">The value of the tag as type <typeparamref name="T"/>.</param>
    public virtual void SetValue(T value)
    {
      Value = value;
    }

    /// <summary>
    /// Sets the value of the tag.
    /// </summary>
    /// <param name="value">The value of the tag as an <see cref="object"/>.</param>
    public virtual void SetValue(object value)
    {
      if (value is T tVal)
        SetValue(tVal);
      else
      {
        var objVal = ConvertValue(value);

        if (objVal is T tObjVal)
          SetValue(tObjVal);
      }
    }

    /// <summary>
    /// Converts the specified value to an <see cref="object"/> of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">The value of the tag as an <see cref="object"/>.</param>
    /// <returns>The value of the tag after conversion.</returns>
    public virtual object ConvertValue(object value)
    {
      return value;
    }

    /// <summary>
    /// Clears the current value of the tag.
    /// </summary>
    public virtual void ClearValue()
    {
      this.Value = default(T);
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="object"/> value is valid for 
    /// the current tag.
    /// </summary>
    /// <param name="value">The value of the tag as an <see cref="object"/>.</param>
    /// <returns><see cref="true"/> if the value is valid for the current tag, else <see cref="false"/>.</returns>
    public virtual bool ValidateValue(object value)
    {
      return !(value is null);
    }

    /// <summary>
    /// Determines whether or not the current tag value is valid.
    /// </summary>
    /// <returns><see cref="true"/> if the current value is valid, else <see cref="false"/>.</returns>
    public virtual bool ValidateValue()
    {
      return ValidateValue(Value);
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual object GetValue()
    {
      return this.Value;
    }

    /// <summary>
    /// Determines whether or not the current tag has a value.
    /// </summary>
    public virtual bool HasValue()
    {
      return !(Value == null || string.IsNullOrEmpty(TextValue));
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="Tag{T}"/> object is equal to the current instance.
    /// </summary>
    /// <param name="tag">The <see cref="Tag{T}"/> object to compare to the current instance.</param>
    /// <returns><see cref="true"/> if the <see cref="Tag{T}"/> object is equal to the current 
    /// instance, else <see cref="false"/>.</returns>
    public virtual bool Equals(Tag<T> tag)
    {
      if (tag is null)
        return false;

      return (tag.Name ?? string.Empty).Equals(Name ?? string.Empty) &&
             tag.GetType().Equals(GetType()) &&
             (TextValue ?? string.Empty).Equals(TextValue ?? string.Empty) &&
             (tag.ValueLength ?? 0).Equals(ValueLength ?? 0);

    }

    /// <summary>
    /// Clones the current instance.
    /// </summary>
    public virtual object Clone()
    {
      return MemberwiseClone();
    }

    /// <summary>
    /// Calculates the hash code for the current instance.
    /// </summary>
    public override int GetHashCode()
    {
      unchecked
      {
        const int hashingBase = (int)2166136261;
        const int hashingMultiplier = 16777619;

        var hash = hashingBase;
        hash = (hash * hashingMultiplier) ^ (!(Name is null) ? Name.GetHashCode() : 0);
        hash = (hash * hashingMultiplier) ^ (GetType().GetHashCode());
        hash = (hash * hashingMultiplier) ^ (!(TextValue is null) ? TextValue.GetHashCode() : 0);
        hash = (hash * hashingMultiplier) ^ (!(ValueLength is null) ? ValueLength.GetHashCode() : 0);

        if (this is UserDefTag)
        {
          var userDefTag = this as UserDefTag;
          hash = (hash * hashingMultiplier) ^ userDefTag.FieldId.GetHashCode();
          hash = (hash * hashingMultiplier) ^ userDefTag.FieldName.GetHashCode();
        }

        return hash;
      }
    }

    /// <summary>
    /// Determines whether or not the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare to the current instance.</param>
    /// <returns><see cref="true"/> if the object is equal to the current instance, 
    /// else <see cref="false"/>.</returns>
    public override bool Equals(object obj)
    {
      if (obj is null)
        return false;

      if (obj is Tag<T> tag)
        return Equals(tag);

      return false;
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of the current instance.
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of the current instance.
    /// </summary>
    /// <param name="format">The value that determines the format of the object as a <see cref="string"/>.</param>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a <see cref="string"/> representation of the current instance.
    /// </summary>
    /// <param name="format">The value that determines the format of the object as a <see cref="string"/>.</param>
    /// <param name="provider">The culture-specific <see cref="IFormatProvider"/> object.</param>
    /// <returns>A <see cref="string"/> representation of the current instance.</returns>
    public virtual string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format)
      {
        case "G":
        case "N":
          return Name ?? string.Empty;

        case "n":
          return ToString("N", provider).ToLower();

        case "L":
          return ValueLength.HasValue ? ((int)ValueLength).ToString(provider) : string.Empty;

        case "t":
          return GetType().Name;

        case "T":
          return typeof(T).FullName;

        case "V":
          return TextValue ?? string.Empty;

        case "A":
          var upperVal = string.Empty;

          if (!string.IsNullOrEmpty(Name))
          {
            upperVal = $"{Values.TAG_OPENING}{ToString("N", provider)}";

            if (!SuppressLength)
              upperVal = $"{upperVal}{Values.VALUE_LENGTH_CHAR}{ValueLength}";

            upperVal = $"{upperVal}{Values.TAG_CLOSING}{TextValue} ";
          }
          return upperVal;

        case "a":
          var lowerVal = string.Empty;

          if (!string.IsNullOrEmpty(Name))
          {
            lowerVal = $"{Values.TAG_OPENING}{ToString("n", provider)}";

            if (!SuppressLength)
              lowerVal = $"{lowerVal}{Values.VALUE_LENGTH_CHAR}{ValueLength}";

            lowerVal = $"{lowerVal}{Values.TAG_CLOSING}{TextValue} ";
          }
          return lowerVal;

        default:
          throw new FormatException($"Format string '{format}' is not valid for type {GetType().Name}.");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual XmlElement ToXml(XmlDocument document)
    {
      if (document == null)
        return null;

      var el = document.CreateElement(Name);
      el.InnerText = TextValue;
      return el;
    }
  }
}

