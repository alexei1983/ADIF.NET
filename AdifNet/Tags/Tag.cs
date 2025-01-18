using System.Globalization;
using System.Xml;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Tags
{
    /// <summary>
    /// The base class from which all ADIF.NET tags inherit.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the underlying value of the tag.</typeparam>
    public class Tag<T> : ITag, IFormattable, IEquatable<Tag<T>>, ICloneable
    {
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
                var val = Value;

                string? textValue;
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
        public virtual IAdifType AdifType { get; }

        /// <summary>
        /// ADIF data type indicator.
        /// </summary>
        public virtual string DataType
        {
            get
            {
                return AdifType != null ? AdifType.Type ?? string.Empty : string.Empty;
            }
        }

        /// <summary>
        /// String that delimits values in a multivalued ADIF tag.
        /// </summary>
        public virtual string ValueSeparator { get; set; }

        /// <summary>
        /// The value of the tag as an <see cref="object"/>.
        /// </summary>
        object? ITag.Value { get { return Value; } }

        /// <summary>
        /// The strongly-typed value of the tag as type <typeparamref name="T"/>.
        /// </summary>
        public virtual T? Value { get; private set; }

        /// <summary>
        /// The string used to format the text value of the tag.
        /// </summary>
        public virtual string? FormatString { get; set; }

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
        public virtual AdifEnumeration Options { get; } = [];

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
        /// Whether or not the tag is an application-defined tag.
        /// </summary>
        public virtual bool IsAppDef { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="Tag{T}"/> class.
        /// </summary>
        public Tag()
        {
            Name = string.Empty;
            AdifType = AdifType<T>.Default;
            ValueSeparator = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Tag{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The value of the tag as type <typeparamref name="T"/>.</param>
        public Tag(T value) : this()
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
        public virtual void SetValue(object? value)
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
        public virtual object? ConvertValue(object? value)
        {
            return value;
        }

        /// <summary>
        /// Clears the current value of the tag.
        /// </summary>
        public virtual void ClearValue()
        {
            Value = default;
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="object"/> value is valid for 
        /// the current tag.
        /// </summary>
        /// <param name="value">The value of the tag as an <see cref="object"/>.</param>
        /// <returns>True if the value is valid for the current tag, else false.</returns>
        public virtual bool ValidateValue(object? value)
        {
            return value is not null;
        }

        /// <summary>
        /// Determines whether or not the current tag value is valid.
        /// </summary>
        /// <returns>True if the current value is valid, else false.</returns>
        public virtual bool ValidateValue()
        {
            return ValidateValue(Value);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual object? GetValue()
        {
            return Value;
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
        /// <returns>True if the <see cref="Tag{T}"/> object is equal to the current 
        /// instance, else false.</returns>
        public virtual bool Equals(Tag<T>? tag)
        {
            if (tag is null)
                return false;

            if (this is UserDefTag thisUserDef && tag is UserDefTag userDef)
            {
                if (!thisUserDef.FieldId.Equals(userDef.FieldId) ||
                  !thisUserDef.FieldName.Equals(userDef.FieldName, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return (tag.Name ?? string.Empty).Equals(Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
                   tag.GetType().Equals(GetType()) &&
                   (TextValue ?? string.Empty).Equals(TextValue ?? string.Empty, StringComparison.OrdinalIgnoreCase) &&
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
                hash = (hash * hashingMultiplier) ^ (Name is not null ? Name.ToUpperInvariant().GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (GetType().GetHashCode());
                hash = (hash * hashingMultiplier) ^ (TextValue is not null ? TextValue.ToUpperInvariant().GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (ValueLength is not null ? ValueLength.GetHashCode() : 0);

                if (this is UserDefTag userDefTag)
                {
                    hash = (hash * hashingMultiplier) ^ userDefTag.FieldId.GetHashCode();
                    hash = (hash * hashingMultiplier) ^ userDefTag.FieldName.ToUpperInvariant().GetHashCode();
                }

                return hash;
            }
        }

        /// <summary>
        /// Determines whether or not the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare to the current instance.</param>
        /// <returns>True if the object is equal to the current instance, else false.</returns>
        public override bool Equals(object? obj)
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
        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a <see cref="string"/> representation of the current instance.
        /// </summary>
        /// <param name="format">The value that determines the format of the object as a <see cref="string"/>.</param>
        /// <param name="provider">The culture-specific <see cref="IFormatProvider"/> object.</param>
        /// <returns>A <see cref="string"/> representation of the current instance.</returns>
        public virtual string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            provider ??= CultureInfo.CurrentCulture;

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
                    return GetType().FullName ?? string.Empty;

                case "V":
                    return TextValue ?? string.Empty;

                case "A":
                    var upperVal = string.Empty;

                    if (!string.IsNullOrEmpty(Name))
                    {
                        upperVal = $"{AdifConstants.TagOpen}{ToString("N", provider)}";

                        if (!SuppressLength)
                            upperVal = $"{upperVal}{AdifConstants.ValueLengthIndicator}{ValueLength}";

                        upperVal = $"{upperVal}{AdifConstants.TagClose}{TextValue} ";
                    }
                    return upperVal;

                case "a":
                    var lowerVal = string.Empty;

                    if (!string.IsNullOrEmpty(Name))
                    {
                        lowerVal = $"{AdifConstants.TagOpen}{ToString("n", provider)}";

                        if (!SuppressLength)
                            lowerVal = $"{lowerVal}{AdifConstants.ValueLengthIndicator}{ValueLength}";

                        lowerVal = $"{lowerVal}{AdifConstants.TagClose}{TextValue} ";
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
        public virtual XmlElement? ToXml(XmlDocument document)
        {
            if (document == null)
                return null;

            var el = document.CreateElement(Name);
            el.InnerText = TextValue;
            return el;
        }
    }
}

