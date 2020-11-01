using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using ADIF.NET.Tags;

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
    /// <param name="value"></param>
    /// <returns></returns>
    public static (double, double) SplitDouble(this double value) {

      var values = value.ToString().Split('.');

      var firstValue = 0d;
      var secondValue = 0d;

      if (values.Length == 2) {
        firstValue = double.Parse(values[0]);
        secondValue = double.Parse(values[1]);
        }
      else {
        firstValue = value;
        }
      
      return (firstValue, secondValue);
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
    /// Determines whether the specified <see cref="DateTime"/> value is after the current value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="compareWith">Value to compare with.</param>
    /// <returns>
    /// 	<see cref="true"/> if the specified current is after; otherwise, <see cref="false"/>.
    /// </returns>
    public static bool IsAfter(this DateTime current, DateTime compareWith) {
      return current > compareWith;
      }

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> is before the current value.
    /// </summary>
    /// <param name="current">The current value.</param>
    /// <param name="compareWith">Value to compare with.</param>
    /// <returns>
    /// 	<see cref="true"/> if the specified current is before; otherwise, <see cref="false"/>.
    /// </returns>
    public static bool IsBefore(this DateTime current, DateTime compareWith) {
      return current < compareWith;
      }

    /// <summary>
    /// Determine if a <see cref="DateTime"/> is in the future.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to be checked.</param>
    /// <param name="utc">Whether or not the <see cref="DateTime"/> represents Universal Coordinated Time (UTC).</param>
    /// <returns><see cref="true"/> if the specified <see cref="DateTime"/> is in the future; otherwise <see cref="false"/>.</returns>
    public static bool IsInFuture(this DateTime dateTime, bool utc = false) {
      return utc ? dateTime > DateTime.UtcNow : dateTime > DateTime.Now;
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
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static string ToKeyPairString<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {

      var retVal = string.Empty;

      if (dictionary == null || dictionary.Count == 0)
        return retVal;

      foreach (KeyValuePair<TKey, TValue> entry in dictionary) {
        retVal = $"{retVal}{Environment.NewLine}Key: {entry.Key}, Value: {entry.Value}";
        }

      return retVal;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ToAdifBoolean(this string str) {
      return ToNullableBooleanAdif(str) ?? false;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsEvenNumber(this int number) {
      return IsEvenNumber((long)number);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsEvenNumber(this long number) {
      return number % 2 == 0;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsOddNumber(this long number) {
      return !number.IsEvenNumber();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsOddNumber(this int number) {
      return IsOddNumber((long)number);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToAdifBooleanValue(this bool? value) {

      if (value.HasValue) {

        if (value == true)
          return BooleanValue.Yes;
        else if (value == false)
          return BooleanValue.No;
        }

      return string.Empty;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToAdifBooleanValue(this bool value) {

      var val = ((bool?)value).ToAdifBooleanValue();
      return !string.IsNullOrEmpty(val) ? val : BooleanValue.No;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool? ToNullableBooleanAdif(this string str) {

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
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ToBoolean(this string str) {
      return ToNullableBoolean(str) ?? false;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool? ToNullableBoolean(this string str) {

      str = (str ?? string.Empty).ToUpper();

      switch (str) {
      case "TRUE":
      case "T":
      case "+":
      case "YES":
      case "Y":
      case "ON":
        return true;

      case "FALSE":
      case "F":
      case "-":
      case "NO":
      case "N":
      case "OFF":
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
             Regex.Match(str, Values.EmailAddressRegex).Success;
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
    public static bool IsAdifNumber(this object obj) {

      if (obj is null)
        return false;

      return obj is double || double.TryParse(obj.ToString(), out double _);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsAdifString(this object obj) {

      if (obj is null)
        obj = string.Empty;

      return obj.ToString().IsAscii();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsAdifMultilineString(this object obj) {

      var objStr = obj?.ToString() ?? string.Empty; 

      return objStr.Contains($"{(char)13}{(char)10}") && objStr.IsAscii();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsAdifIntlMultilineString(this object obj) {

      var objStr = obj?.ToString() ?? string.Empty;

      return objStr.Contains($"{(char)13}{(char)10}");
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="allowEmpty"></param>
    /// <returns></returns>
    public static bool IsAdifGridSquare(this object obj, bool allowEmpty = false) {

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
    public static bool IsAdifPositiveInteger(this object obj) {

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
    public static bool IsAscii(this string str) {
      return Encoding.UTF8.GetByteCount(str) == str.Length;
      }

    /// <summary>
    /// Converts the specified <see cref="object"/> to a value of type <see cref="double"/>.
    /// </summary>
    /// <param name="obj">Object that will be converted.</param>
    /// <returns>Value of type <see cref="double"/> or zero on conversion failure.</returns>
    public static double ToDouble(this object obj) {

      if (!(obj is null)) {

        if (obj is double)
          return (double)obj;
        else if (obj.IsNumber())
          return Convert.ToDouble(obj);
        else if (double.TryParse(obj.ToString(), out double dblVal))
          return dblVal;
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
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="field"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static string[] GetSubOptionsArray<TAttribute>(this FieldInfo field,
                                                          Func<TAttribute, string> valueSelector,
                                                          bool inherit = false) where TAttribute : Attribute {

      return field?.GetSubOptions(valueSelector, inherit)?.ToArray() ?? new string[] { };
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="field"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetSubOptions<TAttribute>(this FieldInfo field,
                                                               Func<TAttribute, string> valueSelector,
                                                               bool inherit = false) where TAttribute: Attribute {

      if (field != null) {

        foreach (var subOptionValue in field.GetAttributeValues(valueSelector, inherit))
          yield return subOptionValue;
        }
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

    /// <summary>Makes a deep copy of the specified object.</summary>
    /// <typeparam name="T"><see cref="Type"/> of the return object.</typeparam>
    /// <param name="obj">Object to be copied.</param>
    /// <returns>The copied object.</returns>
    public static T Clone<T>(this object obj) {

      var result = default(T);

      if (obj is null)
        return result;

      var formatter = new BinaryFormatter();

      using (var stream = new MemoryStream()) {

        formatter.Serialize(stream, obj);
        stream.Seek(0, SeekOrigin.Begin);

        result = (T)formatter.Deserialize(stream);
        }

      return result;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable {

      return list.Select(cloneable => cloneable.Clone()).Select(clonedObj => (T)clonedObj).ToList();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="type"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<TValue> GetAttributeValues<TAttribute, TValue>(this Type type,
                                                                             Func<TAttribute, TValue> valueSelector,
                                                                             bool inherit = true) where TAttribute : Attribute {
      if (valueSelector == null)
        throw new ArgumentNullException(nameof(valueSelector));

      var attributes = GetAttributes<TAttribute>(type, inherit);

      if (attributes != null) {

        foreach (var attribute in attributes)
          yield return valueSelector(attribute);
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="type"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
                                                               Func<TAttribute, TValue> valueSelector,
                                                               bool inherit = true) where TAttribute : Attribute { 
      if (valueSelector == null)
        throw new ArgumentNullException(nameof(valueSelector));

      var attribute = GetAttribute<TAttribute>(type, inherit);

      return attribute != null ? valueSelector(attribute) : default(TValue);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TAttribute GetAttribute<TAttribute>(this Type type,
                                                      bool inherit = true) where TAttribute : Attribute {
      var attributes = GetAttributes<TAttribute>(type, inherit);

      return attributes?.FirstOrDefault();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="type"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Type type,
                                                                    bool inherit = true) where TAttribute : Attribute {
      var attributes = type.GetCustomAttributes(typeof(TAttribute), inherit);

      return attributes.Cast<TAttribute>();
      }

    /// <summary>
    /// Determines whether or not the specified <see cref="string"/> is an IOTA designator.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <returns>True if the <see cref="string"/> is an IOTA designator, else false.</returns>
    public static bool IsIotaDesignator(this string str) {

      var continents = typeof(Continent).GetValuesArray();
      var regex = "^(";

      for (var x = 0; x < continents.Length; x++) {
        regex = $"{regex}{continents[x]}";

        if ((x + 1) < continents.Length)
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
    public static bool IsSotaDesignator(this string str) {

      var regex = @"[A-Za-z0-9]{1,3}\/[A-Za-z]{2,3}\-[0-9]{3}";
      return Regex.IsMatch(str ?? string.Empty, regex);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="field"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TAttribute GetAttribute<TAttribute>(this FieldInfo field,
                                                      bool inherit = true) where TAttribute : Attribute {
      var attributes = GetAttributes<TAttribute>(field, inherit);

      return attributes?.FirstOrDefault();
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
        else if (DateTime.TryParseExact(obj.ToString() ?? string.Empty,
                                   Values.AdifDateFormat,
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
        return obj.ToString().ToAdifBoolean();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="field"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this FieldInfo field,
                                                                    bool inherit = true) where TAttribute : Attribute {
      var attributes = field.GetCustomAttributes(typeof(TAttribute), inherit);

      return attributes.Cast<TAttribute>();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TValue GetAttributeValue<TAttribute, TValue>(this FieldInfo field,
                                                               Func<TAttribute, TValue> valueSelector,
                                                               bool inherit = true) where TAttribute : Attribute {
      if (valueSelector == null)
        throw new ArgumentNullException(nameof(valueSelector));

      var attribute = GetAttribute<TAttribute>(field, inherit);

      return attribute != null ? valueSelector(attribute) : default(TValue);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <param name="valueSelector"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<TValue> GetAttributeValues<TAttribute, TValue>(this FieldInfo field,
                                                                             Func<TAttribute, TValue> valueSelector,
                                                                             bool inherit = true) where TAttribute : Attribute {
      if (valueSelector == null)
        throw new ArgumentNullException(nameof(valueSelector));

      var attributes = GetAttributes<TAttribute>(field, inherit);

      if (attributes != null) {

        foreach (var attribute in attributes)
          yield return valueSelector(attribute);
        }
      }

    }
  }
