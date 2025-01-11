using System.Globalization;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Represents a value in an ADIF enumeration.
    /// </summary>
    public class AdifEnumerationValue : IFormattable
    {
        /// <summary>
        /// The code for the enumeration value.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The display name of the enumeration value.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Whether or not the enumeration value is only valid on import.
        /// </summary>
        public bool ImportOnly { get; set; }

        /// <summary>
        /// Whether or not the enumeration value is a legacy value.
        /// </summary>
        public bool Legacy { get; set; }

        /// <summary>
        /// The parent code for the enumeration value.
        /// </summary>
        public string? Parent { get; set; }

        /// <summary>
        /// The enumeration type of the parent.
        /// </summary>
        public string? ParentType { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumerationValue"/> class.
        /// </summary>
        /// <param name="code">The code for the enumeration value.</param>
        public AdifEnumerationValue(string code) : this(code, string.Empty) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumerationValue"/> class.
        /// </summary>
        /// <param name="code">The code for the enumeration value.</param>
        /// <param name="displayName">The display name of the enumeration value.</param>
        public AdifEnumerationValue(string code, string displayName) : this(code, displayName, false, false, null, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumerationValue"/> class.
        /// </summary>
        /// <param name="code">The code for the enumeration value.</param>
        /// <param name="displayName">The display name of the enumeration value.</param>
        /// <param name="importOnly">Whether or not the enumeration value is only valid on import.</param>
        /// <param name="legacy">Whether or not the enumeration value is a legacy value.</param>
        /// <param name="parent">The parent code for the enumeration value.</param>
        /// <param name="parentType">The type of the parent type for the enumeration value.</param>
        public AdifEnumerationValue(string code, string displayName, bool importOnly, bool legacy, string? parent = null, string? parentType = null)
        {
            DisplayName = displayName;
            Code = code;
            ImportOnly = importOnly;
            Legacy = legacy;
            Parent = parent;
            ParentType = parentType;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifEnumerationValue"/> class.
        /// </summary>
        /// <param name="value">Dynamic object representing the enumeration value.</param>
        public AdifEnumerationValue(dynamic value)
        {
            if (value is IDictionary<string, object?> dict)
            {
                if (dict.TryGetValue(nameof(DisplayName), out object? displayName) && displayName is string _displayName)
                    DisplayName = _displayName;

                if (dict.TryGetValue(nameof(Code), out var code))
                {
                    if (code is string strCode)
                        Code = strCode;
                    else if (code is int intCode)
                        Code = intCode.ToString();
                    else if (code is double dblCode)
                        Code = dblCode.ToString();
                    else if (code is long lngCode)
                        Code = lngCode.ToString();
                    else
                        Code = code?.ToString() ?? string.Empty;
                }

                if (dict.TryGetValue(nameof(ImportOnly), out object? importOnly))
                {
                    if (importOnly is bool _importOnly)
                        ImportOnly = _importOnly;
                    else if (importOnly is int intImportOnly)
                        ImportOnly = intImportOnly == 1;
                    else if (importOnly is double dblImportOnly)
                        ImportOnly = dblImportOnly == 1;
                    else if (importOnly is long lngImportOnly)
                        ImportOnly = lngImportOnly == 1;
                }

                if (dict.TryGetValue(nameof(Legacy), out object? legacy))
                {
                    if (legacy is bool _legacy)
                        Legacy = _legacy;
                    else if (legacy is int intLegacy)
                        Legacy = intLegacy == 1;
                    else if (legacy is double dblLegacy)
                        Legacy = dblLegacy == 1;
                    else if (legacy is long lngLegacy)
                        Legacy = lngLegacy == 1;
                }

                if (dict.TryGetValue(nameof(Parent), out object? parent) && parent is string _parent)
                    Parent = _parent;

                if (dict.TryGetValue(nameof(ParentType), out object? parentType) && parentType is string _parentType)
                    ParentType = _parentType;
            }

            Code ??= string.Empty;
            DisplayName ??= string.Empty;
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="AdifEnumerationValue"/>.
        /// </summary>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="AdifEnumerationValue"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a string representation of the current <see cref="AdifEnumerationValue"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="provider">Culture-specific format provider.</param>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";

            provider ??= CultureInfo.CurrentCulture;

            return format switch
            {
                "G" or "g" => ToString("E", provider),
                "C" => Code ?? string.Empty,
                "N" => DisplayName ?? string.Empty,
                "I" => ImportOnly.ToString(),
                "L" => Legacy.ToString(),
                "E" => !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(DisplayName) ?
                                      $"{ToString("C", provider)} - {ToString("N", provider)}" :
                                      !string.IsNullOrEmpty(DisplayName) ? ToString("N", provider) :
                                      !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
                                      string.Empty,
                "e" => !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(DisplayName) ?
                                      $"{ToString("N", provider)} - {ToString("C", provider)}" :
                                      !string.IsNullOrEmpty(DisplayName) ? ToString("N", provider) :
                                      !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
                                      string.Empty,
                _ => throw new FormatException($"Format string '{format}' is not valid."),
            };
        }
    }
}
