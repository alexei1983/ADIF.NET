using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Enumeration ADIF type.
  /// </summary>
  public class ADIFEnumerationType : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Enumeration;

    /// <summary>
    /// Whether or not the type is an enumeration.
    /// </summary>
    public override bool IsEnumeration => true;

    /// <summary>
    /// Validates the string representation of an ADIF Enumeration value. 
    /// </summary>
    /// <param name="s">String representation of an ADIF Enumeration value.</param>
    public static string Parse(string s)
    {
      if (!ADIFString.TryParse(s, out string result))
        throw new Exception($"Invalid ADIF Enumeration value: {s}");

      return result;
    }

    /// <summary>
    /// Validates the string representation of an ADIF Enumeration value. 
    /// </summary>
    /// <param name="s">String representation of an ADIF Enumeration value.</param>
    /// <param name="options">Valid enumeration options.</param>
    public static string Parse(string s, ADIFEnumeration options)
    {
      var result = Parse(s);

      if (options != null)
      {
        if (!options.IsValid(result))
          throw new Exception($"Invalid ADIF Enumeration value: {s}");

        var option = options.GetValue(result);
        return option.Code;
      }

      return result;
    }

    /// <summary>
    /// Validates the string representation of an ADIF Enumeration value. 
    /// A return value indicates whether the validation succeeded or failed.
    /// </summary>
    /// <param name="s">String representation of an ADIF Enumeration value.</param>
    /// <param name="result">Result of the conversion or validation operation.</param>
    public static bool TryParse(string s, out string result)
    {
      return ADIFString.TryParse(s, out result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s">String representation of an ADIF Enumeration value.</param>
    /// <param name="options">Valid enumeration options.</param>
    /// <param name="result">Result of the conversion or validation operation.</param>
    public static bool TryParse(string s, ADIFEnumeration options, out string result)
    {
      if (TryParse(s, out result))
      {
        if (options != null)
        {
          if (!options.IsValid(result))
          {
            result = null;
            return false;
          }

          var option = options.GetValue(result);
          result = option.Code;
        }

        return true;
      }

      result = null;
      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="o"></param>
    public static bool IsValidValue(object o)
    {
      return IsValidValue(o == null ? string.Empty : o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool IsValidValue(string s)
    {
      return TryParse(s, out _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="o"></param>
    /// <param name="options"></param>
    public static bool IsValidValue(object o, ADIFEnumeration options)
    {
      return IsValidValue(o == null ? string.Empty : o.ToString(), options);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="options"></param>
    public static bool IsValidValue(string s, ADIFEnumeration options)
    {
      return TryParse(s, options, out _);
    }
  }
}
