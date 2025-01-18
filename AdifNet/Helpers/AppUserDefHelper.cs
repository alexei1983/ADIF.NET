using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Tags;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif.Helpers
{
    /// <summary>
    /// Helper class for user-defined and application-defined ADIF tags.
    /// </summary>
    internal static class AppUserDefHelper
    {
        /// <summary>
        /// Converts the specified value to the appropriate type based on the 
        /// specified ADIF data type indicator.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="typeIndicator">ADIF data type indicator.</param>
        public static object? ConvertValueByType(object value, string typeIndicator)
        {
            if (string.IsNullOrWhiteSpace(typeIndicator))
                typeIndicator = string.Empty;

            value ??= string.Empty;

            switch (typeIndicator.ToUpper())
            {
                case DataTypes.Boolean:
                    if (value is bool boolVal)
                        return boolVal;
                    else
                        return new AdifBoolean().Parse(value?.ToString());

                case DataTypes.Number:
                    if (value is double doubleVal)
                        return doubleVal;
                    else
                        return new AdifNumber().Parse(value.ToString());

                case DataTypes.Location:
                    if (value is Location locationVal)
                        return locationVal;
                    else
                        return new AdifLocation().Parse(value.ToString());

                case DataTypes.String:
                    if (value is string strVal)
                        return new AdifString().Parse(strVal);
                    else
                        return new AdifString().Parse(value.ToString());

                case DataTypes.MultilineString:
                    if (value is string multilineStrVal)
                        return new AdifMultilineString().Parse(multilineStrVal);
                    else
                        return new AdifMultilineString().Parse(value.ToString());

                case DataTypes.IntlMultilineString:
                    if (value is string intlMultilineStrVal)
                        return new AdifIntlMultilineString().Parse(intlMultilineStrVal);
                    else
                        return new AdifIntlMultilineString().Parse(value.ToString());

                case DataTypes.IntlString:
                    if (value is string intlStrVal)
                        return new AdifIntlString().Parse(intlStrVal);
                    else
                        return new AdifIntlString().Parse(value.ToString());

                case DataTypes.Enumeration:
                    if (value is AdifEnumerationValue enumVal)
                        return enumVal.Code;
                    else if (value is string enumStrVal)
                        return enumStrVal;
                    else
                        return value.ToString();

                case DataTypes.SponsoredAwardList:
                    if (value is string sponsoredAwardListStr)
                        return new AdifSponsoredAwardList().Parse(sponsoredAwardListStr);
                    else if (value.GetType().IsAssignableFrom(typeof(IEnumerable<string>)))
                        return new List<string>((IEnumerable<string>)value);
                    else
                        return new AdifSponsoredAwardList().Parse(value.ToString());

                case DataTypes.Date:
                    if (value is DateTime dateVal)
                        return dateVal;
                    else
                        return new AdifDate().Parse(value.ToString());

                case DataTypes.Time:
                    if (value is DateTime timeVal)
                        return timeVal;
                    else
                        return new AdifTime().Parse(value.ToString());

                case DataTypes.CreditList:
                    if (value is CreditList creditListVal)
                        return creditListVal;
                    else
                        return new AdifCreditList().Parse(value.ToString());

                default:
                    return value.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeIndicator"></param>
        /// <param name="value"></param>
        public static string GetTextValueByType(string typeIndicator, object? value)
        {
            if (string.IsNullOrEmpty(typeIndicator))
                return value?.ToString() ?? string.Empty;

            switch (typeIndicator.ToUpper())
            {
                case DataTypes.Boolean:
                    return value != null && value is bool boolVal ? boolVal ? AdifConstants.BooleanTrue : AdifConstants.BooleanFalse : string.Empty;

                case DataTypes.Date:
                    return value != null && value is DateTime dateVal ? dateVal.ToString(AdifConstants.DateFormat) : string.Empty;

                case DataTypes.Time:
                    return value != null && value is DateTime timeVal ? timeVal.Second > 0 ? timeVal.ToString(AdifConstants.TimeFormatLong) :
                           timeVal.Second < 1 ? timeVal.ToString(AdifConstants.TimeFormatShort) : string.Empty : string.Empty;

                case DataTypes.String:
                case DataTypes.MultilineString:
                case DataTypes.IntlString:
                case DataTypes.IntlMultilineString:
                    return value != null && value is string strVal ? strVal : string.Empty;

                case DataTypes.Enumeration:
                    return value is AdifEnumerationValue enumVal ? enumVal.Code : value is string enumStr ? enumStr : value != null ? value.ToString() ?? string.Empty : string.Empty;

                case DataTypes.CreditList:
                    if (value != null)
                    {
                        if (value is CreditList creditList)
                            return creditList.ToString();
                        else if (value is string creditStr)
                            return creditStr;
                    }
                    return string.Empty;

                case DataTypes.SponsoredAwardList:
                    if (value != null)
                    {
                        if (value.GetType().IsAssignableFrom(typeof(IEnumerable<string>)))
                            return string.Join(AdifConstants.Comma.ToString(), (IEnumerable<string>)value);
                        else if (value is string awardListStr)
                            return awardListStr;
                    }
                    return string.Empty;

                case DataTypes.Location:
                    return value is Location location ? location.ToString() : value is string locStr ? locStr : string.Empty;

                case DataTypes.Number:
                    return value != null && value.IsNumber() ? value.ToString() ?? string.Empty : string.Empty;

                default:
                    return value is string genericStrVal ? genericStrVal : value != null ? value.ToString() ?? string.Empty : string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeIndicator"></param>
        public static IAdifType? GetAdifType(string typeIndicator)
        {
            if (string.IsNullOrEmpty(typeIndicator))
                return null;

            return typeIndicator.ToUpper() switch
            {
                DataTypes.Boolean => new AdifBoolean(),
                DataTypes.CreditList => new AdifCreditList(),
                DataTypes.Date => new AdifDate(),
                DataTypes.Enumeration => new AdifEnumerationType(),
                DataTypes.IntlMultilineString => new AdifIntlMultilineString(),
                DataTypes.IntlString => new AdifIntlString(),
                DataTypes.Location => new AdifLocation(),
                DataTypes.MultilineString => new AdifMultilineString(),
                DataTypes.Number => new AdifNumber(),
                DataTypes.SponsoredAwardList => new AdifSponsoredAwardList(),
                DataTypes.String => new AdifString(),
                DataTypes.Time => new AdifTime(),
                _ => null,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        public static IAdifType? GetAdifTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;

            return typeName.ToUpper() switch
            {
                DataTypeNames.Boolean => new AdifBoolean(),
                DataTypeNames.CreditList => new AdifCreditList(),
                DataTypeNames.Date => new AdifDate(),
                DataTypeNames.Enumeration => new AdifEnumerationType(),
                DataTypeNames.IntlMultilineString => new AdifIntlMultilineString(),
                DataTypeNames.IntlString => new AdifIntlString(),
                DataTypeNames.Location => new AdifLocation(),
                DataTypeNames.MultilineString => new AdifMultilineString(),
                DataTypeNames.Number => new AdifNumber(),
                DataTypeNames.SponsoredAwardList => new AdifSponsoredAwardList(),
                DataTypeNames.String => new AdifString(),
                DataTypeNames.Time => new AdifTime(),
                DataTypeNames.GridSquare => new AdifGridSquare(),
                DataTypeNames.SotaRef => new AdifSotaRef(),
                DataTypeNames.AwardList => new AdifAwardList(),
                DataTypeNames.SecondaryAdministrativeSubdivisionListAlt => new AdifSecondarySubdivisionListAlt(),
                _ => null,
            };
        }

        /// <summary>
        /// Validates the uniqueness of the field IDs for the specified user-defined tags.
        /// </summary>
        /// <param name="throwExceptions">Whether or not to throw exceptions if invalid field IDs are found.</param>
        /// <param name="tags">User-defined tags that will be validated.</param>
        public static bool ValidateFieldNumbers(bool throwExceptions, params UserDefTag[] tags)
        {
            if (tags == null)
                return true;

            var numbers = new List<int>();
            var exceptions = new List<Exception>();

            foreach (var tag in tags)
            {
                if (tag.FieldId < 1)
                    exceptions.Add(new UserDefTagException("Field ID must be greater than zero.", tag.FieldName));
                else if (tag.FieldId > 0 && numbers.Contains(tag.FieldId))
                    exceptions.Add(new UserDefTagException($"Field ID {tag.FieldId} has already been used.", tag.FieldName));
                else
                    numbers.Add(tag.FieldId);
            }

            if (exceptions.Count > 0 && throwExceptions)
                throw new AggregateException([.. exceptions]);

            return exceptions.Count < 1;
        }

        /// <summary>
        /// Validates the specified user-defined field name.
        /// </summary>
        /// <param name="fieldName">Name of the user-defined field to validate.</param>
        /// <param name="throwExceptions">Whether or not validation exceptions will be thrown.</param>
        /// <param name="validateTagNameMatch"></param>
        public static bool ValidateUserDefFieldName(string fieldName, bool throwExceptions = true, bool validateTagNameMatch = true)
        {
            var exceptions = new List<Exception>();

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                exceptions.Add(new UserDefTagException("User-defined field name cannot be null, empty string, or white space."));
            }
            else
            {
                if (fieldName[0] == ' ' || fieldName[^1] == ' ')
                    exceptions.Add(new UserDefTagException("User-defined field name cannot begin or end with a space.", fieldName));

                if (fieldName.Contains(AdifConstants.CurlyBraceOpen) || fieldName.Contains(AdifConstants.CurlyBraceClose))
                    exceptions.Add(new UserDefTagException("User-defined field name cannot contain curly braces.", fieldName));

                if (fieldName.Contains(AdifConstants.TagOpen) || fieldName.Contains(AdifConstants.TagClose))
                    exceptions.Add(new UserDefTagException("User-defined field name cannot contain angle brackets (greater-than or less-than sign).", fieldName));

                if (fieldName.Contains(AdifConstants.Colon))
                    exceptions.Add(new UserDefTagException("User-defined field name cannot contain a colon.", fieldName));

                if (fieldName.Contains(AdifConstants.Comma))
                    exceptions.Add(new UserDefTagException("User-defined field name cannot contain a comma.", fieldName));

                if (validateTagNameMatch)
                {
                    if (AdifTags.IsTagName(fieldName))
                        exceptions.Add(new UserDefTagException("User-defined field name cannot match the name of a standard ADIF field.", fieldName));
                }
            }

            if (exceptions.Count > 0)
            {
                if (throwExceptions)
                    throw new AggregateException([..exceptions]);

                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullFieldName"></param>
        public static string[] SplitAppDefinedFieldName(string fullFieldName)
        {
            if (string.IsNullOrEmpty(fullFieldName))
                throw new ArgumentException("Application-defined field name is required.", nameof(fullFieldName));

            var parts = fullFieldName.Split(AdifConstants.Underscore);

            if (parts.Length < 3)
                throw new AppDefTagException($"Invalid application-defined field name: {fullFieldName}", fullFieldName);

            if (!AdifTags.AppDef.Equals($"{parts[0] ?? string.Empty}{AdifConstants.Underscore}", StringComparison.OrdinalIgnoreCase))
                throw new AppDefTagException($"Invalid application-defined field name: {fullFieldName}", fullFieldName);

            var newParts = new List<string>() { parts[0], parts[1] };

            var fieldName = string.Empty;
            for (var p = 2; p < parts.Length; p++)
            {
                if (p > 2)
                    fieldName += AdifConstants.Underscore.ToString();

                fieldName += parts[p];
            }

            newParts.Add(fieldName);

            return [.. newParts];
        }

        /// <summary>
        /// Validates the specified program ID to ensure no underscore character is present. The presence of an underscore 
        /// in the program ID results in an ambiguous application-defined field name (i.e. APP_PROGRAM_NAME_VENDOR_NAME: where 
        /// does the program ID portion end and the field name begin?)
        /// </summary>
        /// <param name="programId">Program ID to validate.</param>
        public static void ValidateProgramId(string programId)
        {
            if (string.IsNullOrEmpty(programId))
                throw new AppDefTagException("Program ID is required.");

            if (programId.Contains(AdifConstants.Underscore))
                throw new AppDefTagException("Program ID cannot contain an underscore.");

            if (programId.Contains(AdifConstants.Comma))
                throw new AppDefTagException("Program ID cannot contain a comma.");

            if (programId.Contains(AdifConstants.Colon))
                throw new AppDefTagException("Program ID cannot contain a colon.");

            if (programId.Contains(AdifConstants.TagOpen) || programId.Contains(AdifConstants.TagClose))
                throw new AppDefTagException("Program ID cannot contain an open-angle bracket or close-angle bracket.");

            if (programId.Contains(AdifConstants.CurlyBraceOpen) || programId.Contains(AdifConstants.CurlyBraceClose))
                throw new AppDefTagException("Program ID cannot contain an open-curly brace or close-curly brace.");

            if (programId.EndsWith(' '))
                throw new AppDefTagException("Program ID cannot end with a space.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <exception cref="AppDefTagException"></exception>
        public static void ValidateAppDefFieldName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new AppDefTagException("Application-defined field name is required.");

            if (fieldName.Contains(AdifConstants.Comma))
                throw new AppDefTagException("Application-defined field cannot contain a comma.");

            if (fieldName.Contains(AdifConstants.Colon))
                throw new AppDefTagException("Application-defined field cannot contain a colon.");

            if (fieldName.Contains(AdifConstants.TagOpen) || fieldName.Contains(AdifConstants.TagClose))
                throw new AppDefTagException("Application-defined field cannot contain an open-angle bracket or close-angle bracket.");

            if (fieldName.Contains(AdifConstants.CurlyBraceOpen) || fieldName.Contains(AdifConstants.CurlyBraceClose))
                throw new AppDefTagException("Application-defined field cannot contain an open-curly brace or close-curly brace.");

            if (fieldName.EndsWith(' '))
                throw new AppDefTagException("Application-defined field cannot end with a space.");
        }
    }
}
