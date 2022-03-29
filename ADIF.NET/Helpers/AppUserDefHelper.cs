using System;
using System.Collections.Generic;
using ADIF.NET.Exceptions;
using ADIF.NET.Tags;
using ADIF.NET.Types;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper class for user-defined and application-defined ADIF tags.
  /// </summary>
  public static class AppUserDefHelper {

    /// <summary>
    /// Converts the specified value to the appropriate type based on the 
    /// specified ADIF data type indicator.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="typeIndicator">ADIF data type indicator.</param>
    public static object ConvertValueByType(object value, string typeIndicator)
    {
      if (string.IsNullOrWhiteSpace(typeIndicator))
        typeIndicator = string.Empty;

      value = value ?? string.Empty;

      switch (typeIndicator.ToUpper())
      {
        case DataTypes.Boolean:
          if (value is bool boolVal)
            return boolVal;
          else if (value is bool?)
            return (bool?)value;
          else
            return ADIFBoolean.Parse(value.ToString());

        case DataTypes.Number:
          if (value is double doubleVal)
            return doubleVal;
          else if (value is double?)
            return value as double?;
          else
            return ADIFNumber.Parse(value.ToString());

        case DataTypes.Location:
          if (value is Location locationVal)
            return locationVal;
          else
            return ADIFLocation.Parse(value.ToString());

        case DataTypes.String:
          if (value is string strVal)
            return ADIFString.Parse(strVal);
          else 
            return ADIFString.Parse(value.ToString());

        case DataTypes.MultilineString:
          if (value is string multilineStrVal)
            return ADIFMultilineString.Parse(multilineStrVal);
          else
            return ADIFMultilineString.Parse(value.ToString());

        case DataTypes.IntlMultilineString:
          if (value is string intlMultilineStrVal)
            return ADIFIntlMultilineString.Parse(intlMultilineStrVal);
          else
            return ADIFIntlMultilineString.Parse(value.ToString());

        case DataTypes.IntlString:
          if (value is string intlStrVal)
            return ADIFIntlString.Parse(intlStrVal);
          else
            return ADIFIntlString.Parse(value.ToString());

        case DataTypes.Enumeration:
          if (value is ADIFEnumerationValue enumVal)
            return enumVal.Code;
          else if (value is string enumStrVal)
            return enumStrVal;
          else
            return value.ToString();

        case DataTypes.SponsoredAwardList:
          if (value is string sponsoredAwardListStr)
            return ADIFSponsoredAwardList.Parse(sponsoredAwardListStr);
          else if (value.GetType().IsAssignableFrom(typeof(IEnumerable<string>)))
            return new List<string>((IEnumerable<string>)value).ToArray();
          else
            return ADIFSponsoredAwardList.Parse(value.ToString());

        case DataTypes.Date:
          if (value is DateTime dateVal)
            return dateVal;
          else if (value is DateTime?)
            return (DateTime?)value;
          else
            return ADIFDate.Parse(value.ToString());

        case DataTypes.Time:
          if (value is DateTime timeVal)
            return timeVal;
          else if (value is DateTime?)
            return (DateTime?)value;
          else
            return ADIFTime.Parse(value.ToString());

        case DataTypes.CreditList:
          if (value is CreditList creditListVal)
            return creditListVal;
          else
            return ADIFCreditList.Parse(value.ToString());

        default:
          return value.ToString();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeIndicator"></param>
    /// <param name="value"></param>
    public static string GetTextValueByType(string typeIndicator, object value)
    {
      if (string.IsNullOrEmpty(typeIndicator))
        return value is null ? string.Empty : value.ToString();

      switch (typeIndicator.ToUpper())
      {
        case DataTypes.Boolean:
          return value != null && value is bool boolVal ? boolVal ? Values.ADIF_BOOLEAN_TRUE : Values.ADIF_BOOLEAN_FALSE : string.Empty;

        case DataTypes.Date:
          return value != null && value is DateTime dateVal ? dateVal.ToString(Values.ADIF_DATE_FORMAT) : string.Empty;

        case DataTypes.Time:
          return value != null && value is DateTime timeVal ? timeVal.Second > 0 ? timeVal.ToString(Values.ADIF_TIME_FORMAT_LONG) :
                 timeVal.Second < 1 ? timeVal.ToString(Values.ADIF_TIME_FORMAT_SHORT) : string.Empty : string.Empty;

        case DataTypes.String:
        case DataTypes.MultilineString:
        case DataTypes.IntlString:
        case DataTypes.IntlMultilineString:
          return value != null && value is string strVal ? strVal : string.Empty;

        case DataTypes.Enumeration:
          return value is ADIFEnumerationValue enumVal ? enumVal.Code : value is string enumStr ? enumStr : value != null ? value.ToString() : string.Empty;

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
              return string.Join(Values.COMMA.ToString(), (IEnumerable<string>)value);
            else if (value is string awardListStr)
              return awardListStr;
          }
          return string.Empty;

        case DataTypes.Location:
          return value is Location location ? location.ToString() : value is string locStr ? locStr : string.Empty;

        case DataTypes.Number:
          return value != null && value.IsNumber() ? value.ToString() : string.Empty;

        default:
          return value is string genericStrVal ? genericStrVal : value != null ? value.ToString() : string.Empty;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeIndicator"></param>
    public static IADIFType GetADIFType(string typeIndicator)
    {
      if (string.IsNullOrEmpty(typeIndicator))
        return null;

      switch (typeIndicator.ToUpper())
      {
        case DataTypes.Boolean:
          return new ADIFBoolean();

        case DataTypes.CreditList:
          return new ADIFCreditList();

        case DataTypes.Date:
          return new ADIFDate();

        case DataTypes.Enumeration:
          return new ADIFEnumerationType();

        case DataTypes.IntlMultilineString:
          return new ADIFIntlMultilineString();

        case DataTypes.IntlString:
          return new ADIFIntlString();

        case DataTypes.Location:
          return new ADIFLocation();

        case DataTypes.MultilineString:
          return new ADIFMultilineString();

        case DataTypes.Number:
          return new ADIFNumber();

        case DataTypes.SponsoredAwardList:
          return new ADIFSponsoredAwardList();

        case DataTypes.String:
          return new ADIFString();

        case DataTypes.Time:
          return new ADIFTime();

        default:
          return new ADIFIntlMultilineString();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeName"></param>
    public static IADIFType GetADIFTypeByName(string typeName)
    {
      if (string.IsNullOrEmpty(typeName))
        return null;

      switch (typeName.ToUpper())
      {
        case DataTypeNames.Boolean:
          return new ADIFBoolean();

        case DataTypeNames.CreditList:
          return new ADIFCreditList();

        case DataTypeNames.Date:
          return new ADIFDate();

        case DataTypeNames.Enumeration:
          return new ADIFEnumerationType();

        case DataTypeNames.IntlMultilineString:
          return new ADIFIntlMultilineString();

        case DataTypeNames.IntlString:
          return new ADIFIntlString();

        case DataTypeNames.Location:
          return new ADIFLocation();

        case DataTypeNames.MultilineString:
          return new ADIFMultilineString();

        case DataTypeNames.Number:
          return new ADIFNumber();

        case DataTypeNames.SponsoredAwardList:
          return new ADIFSponsoredAwardList();

        case DataTypeNames.String:
          return new ADIFString();

        case DataTypeNames.Time:
          return new ADIFTime();

        case DataTypeNames.GridSquare:
          return new ADIFGridSquare();

        case DataTypeNames.SOTARef:
          return new ADIFSOTARef();

        case DataTypeNames.AwardList:
          return new ADIFAwardList();

        default:
          return null;
      }
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
        throw new AggregateException(exceptions.ToArray());

      return exceptions.Count < 1;
    }

    /// <summary>
    /// Validates the specified user-defined field name.
    /// </summary>
    /// <param name="fieldName">Name of the user-defined field to validate.</param>
    /// <param name="throwExceptions">Whether or not validation exceptions will be thrown.</param>
    public static bool ValidateFieldName(string fieldName, bool throwExceptions = true, bool validateTagNameMatch = true)
    {
      var exceptions = new List<Exception>();

      if (string.IsNullOrWhiteSpace(fieldName))
      {
        exceptions.Add(new UserDefTagException("User-defined field name cannot be null, empty string, or white space."));    
      } else
      { 
        if (fieldName[0] == ' ' || fieldName[fieldName.Length - 1] == ' ')
          exceptions.Add(new UserDefTagException("User-defined field name cannot begin or end with a space.", fieldName));

        if (fieldName.Contains(Values.CURLY_BRACE_OPEN.ToString()) || fieldName.Contains(Values.CURLY_BRACE_CLOSE.ToString()))
          exceptions.Add(new UserDefTagException("User-defined field name cannot contain curly braces.", fieldName));

        if (fieldName.Contains(Values.TAG_OPENING.ToString()) || fieldName.Contains(Values.TAG_CLOSING.ToString()))
          exceptions.Add(new UserDefTagException("User-defined field name cannot contain angle brackets (greater-than or less-than sign).", fieldName));

        if (fieldName.Contains(Values.COLON.ToString()))
          exceptions.Add(new UserDefTagException("User-defined field name cannot contain a colon.", fieldName));

        if (fieldName.Contains(Values.COMMA.ToString()))
          exceptions.Add(new UserDefTagException("User-defined field name cannot contain a comma.", fieldName));

        if (validateTagNameMatch)
        {
          if (TagNames.IsTagName(fieldName))
            exceptions.Add(new UserDefTagException("User-defined field name cannot match the name of a standard ADIF field.", fieldName));
        }
      }

      if (exceptions.Count > 0)
      {
        if (throwExceptions)
          throw new AggregateException(exceptions.ToArray());

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

      var parts = fullFieldName.Split(Values.UNDERSCORE);

      if (parts.Length < 3)
        throw new AppDefTagException($"Invalid application-defined field name: {fullFieldName}", fullFieldName);

      if (!TagNames.AppDef.Equals($"{parts[0] ?? string.Empty}{Values.UNDERSCORE.ToString()}", StringComparison.OrdinalIgnoreCase))
        throw new AppDefTagException($"Invalid application-defined field name: {fullFieldName}", fullFieldName);

      var newParts = new List<string>() { parts[0], parts[1] };

      var fieldName = string.Empty;
      for (var p = 2; p < parts.Length; p++)
      {
        if (p > 2)
          fieldName += Values.UNDERSCORE.ToString();

        fieldName += parts[p];
      }

      newParts.Add(fieldName);

      return newParts.ToArray();
    }

    /// <summary>
    /// Validates the specified program ID to ensure no underscore character is present. The presence of an underscore 
    /// in the program ID results in an ambigious application-defined field name (i.e. APP_PROGRAM_NAME_VENDOR_NAME: where 
    /// does the program ID portion end and the field name begin?)
    /// </summary>
    /// <param name="programId">Program ID to validate.</param>
    public static void ValidateProgramId(string programId)
    {
      if (programId != null)
        if (programId.Contains(Values.UNDERSCORE.ToString()))
          throw new AppDefTagException("Program ID cannot contain the underscore character.");
    }
  }
}
