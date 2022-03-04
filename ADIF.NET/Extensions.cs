using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ADIF.NET {
  public static class Extensions {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToEpoch(this DateTime dateTime) {
      var offset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);
      var dateTimeOffset = new DateTimeOffset(dateTime, offset);
      return dateTimeOffset.ToUnixTimeSeconds();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long? ToEpoch(this DateTime? dateTime) {

      if (dateTime.HasValue) {
        var offset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime.Value);
        var dateTimeOffset = new DateTimeOffset(dateTime.Value, offset);
        return dateTimeOffset.ToUnixTimeSeconds();
        }

      return null;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unixTimeSeconds"></param>
    /// <param name="utc"></param>
    /// <returns></returns>
    public static DateTime FromEpoch(this long unixTimeSeconds, bool utc = false) {
      var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
      return utc ? dateTimeOffset.DateTime : dateTimeOffset.DateTime.ToLocalTime();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlEncode(this string str) {
      return Uri.EscapeDataString(str ?? string.Empty);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlDecode(this string str) {
      return Uri.UnescapeDataString(str ?? string.Empty);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt32(this string str) {
      if (!string.IsNullOrWhiteSpace(str) && int.TryParse(str, out int intVal))
        return intVal;

      return 0;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double ToDouble(this string str) {
      if (!string.IsNullOrWhiteSpace(str) && double.TryParse(str, out double dblVal))
        return dblVal;

      return 0;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? ToNullableInt32(this string str) {
      if (!string.IsNullOrWhiteSpace(str) && int.TryParse(str, out int intVal))
        return intVal;

      return null;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ToADIFBoolean(this string str) {
      return ToNullableBooleanADIF(str) ?? false;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToADIFBooleanValue(this bool? value) {

      if (value.HasValue) {

        if (value == true)
          return "Y";
        else if (value == false)
          return "N";
        }

      return string.Empty;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToADIFBooleanValue(this bool value) {

      var val = ((bool?)value).ToADIFBooleanValue();
      return !string.IsNullOrEmpty(val) ? val : "N";
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool? ToNullableBooleanADIF(this string str) {

      str = (str ?? string.Empty).ToUpper();

      switch (str) {
      case "Y":
        return true;

      case "N":
        return false;

      default:
        return null;
        }
      }

    /// <summary>
    /// Determines whether or not a string is an email address.
    /// </summary>
    /// <param name="str">String to validate as an email address.</param>
    /// <param name="allowEmptyString">Whether or not to allow null/empty/whitespace string as an email address.</param>
    /// <returns><see cref="true"/> if the string is an email address, else <see cref="false"/>.</returns>
    public static bool IsEmailAddress(this string str, bool allowEmptyString = false) {

      var isNullOrWhiteSpace = string.IsNullOrWhiteSpace(str);

      if (isNullOrWhiteSpace && !allowEmptyString)
        return false;

      return (isNullOrWhiteSpace && allowEmptyString) || 
             Regex.Match(str, Values.EMAIL_ADDRESS_REGEX).Success;
      }

    /// <summary>
    /// Determines whether or not a <see cref="string"/> is numeric.
    /// </summary>
    /// <param name="str">The <see cref="string"/> to validate as numeric.</param>
    /// <returns><see cref="true"/> if the <see cref="string"/> is numeric, else <see cref="false"/>.</returns>
    public static bool IsStringNumeric(this string str) {
      return long.TryParse(str, out long _) || 
        double.TryParse(str, out double _) || decimal.TryParse(str, out decimal _);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsADIFNumber(this object obj) {

      if (obj is null)
        return false;

      return obj is double || double.TryParse(obj.ToString(), out double _);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsADIFString(this object obj) {

      if (obj is null)
        obj = string.Empty;

      return obj.ToString().IsASCII();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsADIFMultilineString(this object obj) {

      var objStr = obj?.ToString() ?? string.Empty; 

      return objStr.Contains($"{(char)13}{(char)10}") && objStr.IsASCII();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsADIFIntlMultilineString(this object obj) {

      var objStr = obj?.ToString() ?? string.Empty;

      return objStr.Contains($"{(char)13}{(char)10}");
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="allowEmpty"></param>
    /// <returns></returns>
    public static bool IsADIFGridSquare(this object obj, bool allowEmpty = false) {

      var objStr = obj?.ToString() ?? string.Empty;
      var objStrLen = objStr.Length;
      var isNullOrEmpty = string.IsNullOrEmpty(objStr);

      if (isNullOrEmpty && !allowEmpty)
        return false;
      
      return objStrLen == 2 || objStrLen == 4 || objStrLen == 6 || objStrLen == 8 || isNullOrEmpty;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsADIFPositiveInteger(this object obj) {

      if (obj is null)
        return false;

      var val = default(double);

      try {
        if (obj is double)
          val = (double)obj;
        else
          val = Convert.ToDouble(obj);
        }
      catch {
        return false;
        }

      return val > 0;
      }

    /// <summary>
    /// Determines whether or not an <see cref="object"/> is a number.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to validate as a number.</param>
    /// <returns><see cref="true"/> if the <see cref="object"/> is a number, else <see cref="false"/>.</returns>
    public static bool IsNumber(this object obj) {

      if (obj is null)
        return false;

      return obj is sbyte
                || obj is byte
                || obj is short
                || obj is ushort
                || obj is int
                || obj is uint
                || obj is long
                || obj is ulong
                || obj is float
                || obj is double
                || obj is decimal;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsASCII(this string str) {
      return Encoding.UTF8.GetByteCount(str) == str.Length;
      }

    /// <summary>
    /// Converts the specified <see cref="object"/> to a value of type <see cref="double"/>.
    /// </summary>
    /// <param name="obj">Object that will be converted.</param>
    /// <returns>Value of type <see cref="double"/> or zero on conversion failure.</returns>
    public static double ToDouble(this object obj) {

      if (!(obj is null)) {

        if (obj is double dblVal)
          return dblVal;
        else if (obj.IsNumber())
          return Convert.ToDouble(obj);
        else if (double.TryParse(obj.ToString(), out double parsedDblVal))
          return parsedDblVal;
        }

      return 0;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string[] GetValuesArray(this Type type) {
      return type?.GetValues()?.ToArray() ?? new string[] { };
      }

    /// <summary>
    /// Retrieves enumeration options from the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> for which enumeration options will be retrieved.</param>
    /// <returns>Array of <see cref="string"/> values representing the available enumeration options from the <see cref="Type"/>.</returns>
    public static IEnumerable<string> GetValues(this Type type) {

      var consts = type.GetConstants(typeof(string));

      foreach (var @const in consts) {

        var val = @const.GetRawConstantValue()?.ToString();
        
        if (!string.IsNullOrEmpty(val))
          yield return val;
        }
      }

    /// <summary>
    /// Retrieves <see cref="FieldInfo"/> objects representing the defined constant values from the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> for which defined constant <see cref="FieldInfo"/> values will be retrieved.</param>
    /// <param name="typeOnly">If specified, only constant values of this <see cref="Type"/> will be retrieved.</param>
    /// <returns><see cref="FieldInfo"/> objects representing the defined <see cref="const"/> values in the <see cref="Type"/>.</returns>
    public static IEnumerable<FieldInfo> GetConstants(this Type type, Type typeOnly = null) {

      var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

      foreach (var fi in fieldInfos) {

        if (typeOnly != null && fi.FieldType != typeOnly)
          continue;

        if (fi.IsLiteral && !fi.IsInitOnly)
          yield return fi;
        }
      }

    /// <summary>
    /// Determines whether or not the specified <see cref="string"/> is an IOTA designator.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <returns>True if the <see cref="string"/> is an IOTA designator, else false.</returns>
    public static bool IsIOTADesignator(this string str) {

      var continents = Values.Continents;
      var regex = "^(";

      for (var x = 0; x < continents.Count; x++) {
        regex = $"{regex}{continents[x].Code}";

        if ((x + 1) < continents.Count)
          regex = $"{regex}|";
        }

      regex = $@"{regex})\-[0-9]{{3}}$";

      return Regex.IsMatch(str ?? string.Empty, regex);
      }

    /// <summary>
    /// Determines whether or not the specified <see cref="string"/> is a SOTA designator.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <returns>True if the <see cref="string"/> is a SOTA designator, else false.</returns>
    public static bool IsSOTADesignator(this string str) {

      var regex = @"[A-Za-z0-9]{1,3}\/[A-Za-z]{2,3}\-[0-9]{3}";
      return Regex.IsMatch(str ?? string.Empty, regex);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this object obj, IFormatProvider provider = null) {

      if (!(obj is null)) {

        if (obj is DateTime dateTime)
          return dateTime;
        else if (DateTime.TryParseExact(obj.ToString(),
                                   Values.ADIF_DATE_FORMAT,
                                   provider ?? CultureInfo.CurrentCulture,
                                   DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite,
                                   out DateTime dateTimeParsed))
          return dateTimeParsed;
        else {
          try {
            var convertedDateTime = Convert.ToDateTime(obj);
            return convertedDateTime;
            }
          catch {
            }
          }
        }

      return default(DateTime);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool ToBoolean(this object obj) {

      if (obj is null)
        return false;

      if (obj is bool boolValue)
        return boolValue;
      else if (obj is bool?)
        return (bool?)obj ?? false;
      else
        return obj.ToString().ToADIFBoolean();
      }

    }
  }
