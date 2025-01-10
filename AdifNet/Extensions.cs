using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Extension methods for ADIF.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts the specified string to an <see cref="int"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        public static int ToInt32(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str) && int.TryParse(str, out int intVal))
                return intVal;

            return 0;
        }

        /// <summary>
        /// Converts the specified string to a <see cref="double"/>.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str) && double.TryParse(str, out double dblVal))
                return dblVal;

            return 0;
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="double"/> is a whole number.
        /// </summary>
        /// <param name="doubleVal">Double value to check.</param>
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
            s ??= string.Empty;
            return s.Contains(Environment.NewLine) || s.Contains(Values.CARRIAGE_RETURN) || s.Contains(Values.NEWLINE);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="object"/> is a whole number.
        /// </summary>
        /// <param name="val">Object to check.</param>
        public static bool IsWholeNumber(this object val)
        {
            if (val is null)
                return false;

            if (!val.IsNumber(out Type? type))
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
        /// Determines whether or not the specified nullable <see cref="double"/> is a whole number.
        /// </summary>
        /// <param name="doubleVal">Nullable double value to check.</param>
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
        public static bool IsAdifGridSquare(this object obj, bool allowEmpty = false)
        {
            var objStr = obj?.ToString() ?? string.Empty;
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
            return IsNumber(obj, out Type? _);
        }

        /// <summary>
        /// Determines whether or not an <see cref="object"/> is a number.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to validate as a number.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>True if the <see cref="object"/> is a number, else false.</returns>
        public static bool IsNumber(this object obj, out Type? type)
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
        /// Determines whether or not the specified string contains only ASCII characters.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>True if the string contains only ASCII characters, else false.</returns>
        public static bool IsAscii(this string str)
        {
            return Encoding.UTF8.GetByteCount(str) == str.Length;
        }

        /// <summary>
        /// Retrieves <see cref="FieldInfo"/> objects representing the defined constant values from the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> for which defined constant <see cref="FieldInfo"/> values will be retrieved.</param>
        /// <param name="typeOnly">If specified, only constant values of this <see cref="Type"/> will be retrieved.</param>
        /// <returns><see cref="FieldInfo"/> objects representing the defined constant values in the <see cref="Type"/>.</returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type type, Type? typeOnly = null)
        {
            var fieldInfo = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (var fi in fieldInfo)
            {
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
        public static bool IsIotaDesignator(this string str)
        {
            var continents = Values.Continents;
            var regex = "^(";

            for (var x = 0; x < continents.Count; x++)
            {
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
        public static bool IsSotaDesignator(this string str)
        {
            return SotaRegex().IsMatch(str ?? string.Empty);
        }

        [GeneratedRegex(Values.SOTA_REF_REGEX)]
        private static partial Regex SotaRegex();
    }
}
