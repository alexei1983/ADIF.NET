using System;
using System.Collections.Generic;
using System.Globalization;

namespace ADIF.NET.Tags {

  public class OldTag : OldITag, IFormattable, IEquatable<OldTag> {

    public virtual string Name { get; }

    public virtual string TextValue {
      get {

        var textValue = string.Empty;

        if (Value?.Length == 1) {

          var val = Value[0];

          if (val is IFormattable formattable)
            textValue = formattable.ToString(FormatString, FormatProvider ?? CultureInfo.CurrentCulture) ?? string.Empty;
          else
            textValue = val?.ToString() ?? string.Empty;
          }
        else if (Value?.Length > 1) {

          foreach (var val in Value) {

            var localVal = string.Empty;

            if (val is IFormattable formattable)
              localVal = formattable.ToString(FormatString, FormatProvider ?? CultureInfo.CurrentCulture) ?? string.Empty;
            else
              localVal = val?.ToString() ?? string.Empty;

            if (!string.IsNullOrEmpty(localVal)) {

              if (string.IsNullOrEmpty(textValue))
                textValue = localVal;
              else
                textValue = $"{textValue}{ValueSeparator ?? Values.DefaultValueSeparator.ToString()}{localVal}";
              }
            }
          }

        return textValue;
        }
      }

    public virtual bool Header { get; }

    public virtual string ValueSeparator { get; set; }

    public object[] Value {

      get {
        if (values == null)
          values = new List<object>();

        return values.ToArray();
        }
      }

    public virtual string FormatString { get; set; }

    public IFormatProvider FormatProvider { get; set; } = CultureInfo.CurrentCulture;

    public virtual Type ExpectedValueType => typeof(object);

    public virtual int? ValueLength {
      get {

        if (!SuppressLength)
          return TextValue.Length;
        else
          return null;
        }
      }

    public int ValueCount => Value.Length;

    public virtual string[] Options { get; }

    public virtual bool RestrictOptions { get; }

    public virtual bool SuppressLength { get; }

    public OldTag() {
      }

    public OldTag(object value) {
      AddValue(value);
      }

    public OldTag(params object[] values) {
      AddValues(values);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public virtual void AddValue(object value) {

      if (value == null)
        return;

      if (values == null)
        values = new List<object>();

      values.Add(value);    
      }

    public virtual void AddValues(params object[] values) {

      if (values == null)
        return;

      foreach (var val in values)
        AddValue(val);
      }

    public virtual void ClearValues() {
      values = new List<object>();
      }

    public virtual bool ValidateValue(object value) {
      return !(value is null);
      }

    public virtual string Build() {

      var retVal = string.Empty;

      if (!string.IsNullOrEmpty(Name)) {
        retVal = $"{Values.TagOpening}{Name}";

        if (!SuppressLength)
          retVal = $"{retVal}{Values.ValueLengthChar}{ValueLength}";

        retVal = $"{retVal}{Values.TagClosing}{TextValue} ";
        }
      return retVal;
      }

    public virtual bool Equals(OldTag tag) {

      if (tag is null)
        return false;

      return (tag.Name ?? string.Empty).Equals(Name ?? string.Empty) &&
             tag.GetType().Equals(GetType()) &&
             (TextValue ?? string.Empty).Equals(TextValue ?? string.Empty) &&
             (tag.ValueLength ?? 0).Equals(ValueLength ?? 0);

      }

    public override int GetHashCode() {

      unchecked {
        
        const int hashingBase = (int)2166136261;
        const int hashingMultiplier = 16777619;

        var hash = hashingBase;
        hash = (hash * hashingMultiplier) ^ (!(Name is null) ? Name.GetHashCode() : 0);
        hash = (hash * hashingMultiplier) ^ (GetType().GetHashCode());
        hash = (hash * hashingMultiplier) ^ (!(TextValue is null) ? TextValue.GetHashCode() : 0);
        hash = (hash * hashingMultiplier) ^ (!(ValueLength is null) ? ValueLength.GetHashCode() : 0);
        return hash;
        }
      }

    public override bool Equals(object obj) {
      
      if (obj is null)
        return false;

      if (obj is OldTag tag)
        return Equals(tag);

      return false;
      }

    public virtual string ToString(string format, IFormatProvider provider) {

      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format) {

      case "G":
      case "N":
        return Name ?? string.Empty;

      case "L":
        return ValueLength.HasValue ? ((int)ValueLength).ToString(provider) : string.Empty;

      case "V":
        return TextValue ?? string.Empty;

      default:
        throw new FormatException($"Format string '{format}' is not valid for type {GetType().Name}.");
        }
      }

    List<object> values;
    }
  }

