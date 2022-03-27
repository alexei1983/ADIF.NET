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
    /// <param name="str"></param>
    public static int ToInt32(this string str)
    {
      if (!string.IsNullOrWhiteSpace(str) && int.TryParse(str, out int intVal))
        return intVal;

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double ToDouble(this string str)
    {
      if (!string.IsNullOrWhiteSpace(str) && double.TryParse(str, out double dblVal))
        return dblVal;

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doubleVal"></param>
    public static bool IsWholeNumber(this double doubleVal)
    {
      return Math.Abs(doubleVal % 1) <= (double.Epsilon * 100);
    }

    /// <summary>
    /// Determines whether or not the specified string contains line ending characters.
    /// </summary>
    /// <param name="s">String to check.</param>
    public static bool HasLineEnding(this string s)
    {
      s = s ?? string.Empty;
      return s.Contains(Environment.NewLine) || s.Contains(Values.CARRIAGE_RETURN) || s.Contains(Values.NEWLINE);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="val"></param>
    public static bool IsWholeNumber(this object val)
    {
      if (val is null)
        return false;

      if (!val.IsNumber(out Type type))
        return false;

      if (type == typeof(decimal) || type == typeof(double) || type == typeof(float))
      {
        return IsWholeNumber((double)val);
      }
      else
      {
        return true;
      } 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doubleVal"></param>
    public static bool IsWholeNumber(this double? doubleVal)
    {
      if (!doubleVal.HasValue)
        return true;

      return doubleVal.Value.IsWholeNumber();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="allowEmpty"></param>
    /// <returns></returns>
    public static bool IsADIFGridSquare(this object obj, bool allowEmpty = false)
    {
      var objStr = obj?.ToString() ?? string.Empty;
      var objStrLen = objStr.Length;
      var isNullOrEmpty = string.IsNullOrEmpty(objStr);

      if (isNullOrEmpty && !allowEmpty)
        return false;

      if (!isNullOrEmpty)
      {
        try
        {
          Unclassified.Util.MaidenheadLocator.LocatorToLatLng(objStr);
          return true;
        }
        catch
        {
          return false;
        }
      }

      return isNullOrEmpty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public static bool IsNumber(this object obj)
    {
      return IsNumber(obj, out Type _);
    }

    /// <summary>
    /// Determines whether or not an <see cref="object"/> is a number.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to validate as a number.</param>
    /// <returns><see cref="true"/> if the <see cref="object"/> is a number, else <see cref="false"/>.</returns>
    public static bool IsNumber(this object obj, out Type type)
    {
      type = null;

      if (obj is null)
        return false;

      type = obj.GetType();

      if (obj is sbyte)
        return true;
      else if (obj is byte)
        return true;
      else if (obj is short)
        return true;
      else if (obj is ushort)
        return true;
      else if (obj is int)
        return true;
      else if (obj is uint)
        return true;
      else if (obj is long)
        return true;
      else if (obj is ulong)
        return true;
      else if (obj is float)
        return true;
      else if (obj is double)
        return true;
      else if (obj is decimal)
        return true;
      else
      {
        type = null;
        return false;
      }
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
      return Regex.IsMatch(str ?? string.Empty, Values.SOTA_REF_REGEX);
      }
    }
  }
