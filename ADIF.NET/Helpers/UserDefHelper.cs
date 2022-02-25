using System;
using System.Collections.Generic;
using ADIF.NET.Tags;
using ADIF.NET.Types;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper class for user-defined ADIF tags.
  /// </summary>
  public static class UserDefHelper {

    /// <summary>
    /// Converts the specified value to the appropriate type based on the 
    /// specified ADIF data type indicator.
    /// </summary>
    /// <param name="value">Value to convert.</param>
    /// <param name="adifType">ADIF data type indicator.</param>
    public static object ConvertValueByType(object value, string adifType)
    {
      if (string.IsNullOrWhiteSpace(adifType))
        adifType = string.Empty;

      value = value == null ? string.Empty : value;

      switch (adifType.ToUpper())
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
          exceptions.Add(new Exception($"Field ID must be greater than zero for user-defined tag '{(tag.FieldName ?? string.Empty)}'."));
        else if (tag.FieldId > 0 && numbers.Contains(tag.FieldId))
          exceptions.Add(new Exception($"Field ID {tag.FieldId} has already been used."));
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
        exceptions.Add(new Exception("User-defined field name cannot be null, empty string, or white space."));    
      } else
      { 
        if (fieldName[0] == ' ' || fieldName[fieldName.Length - 1] == ' ')
          exceptions.Add(new Exception("User-defined field name cannot begin or end with a space."));

        if (fieldName.Contains(Values.CURLY_BRACE_OPEN.ToString()) || fieldName.Contains(Values.CURLY_BRACE_CLOSE.ToString()))
          exceptions.Add(new Exception("User-defined field name cannot contain curly braces."));

        if (fieldName.Contains(Values.TAG_OPENING.ToString()) || fieldName.Contains(Values.TAG_CLOSING.ToString()))
          exceptions.Add(new Exception("User-defined field name cannot contain angle brackets (greater-than or less-than sign)."));

        if (fieldName.Contains(Values.COLON.ToString()))
          exceptions.Add(new Exception("User-defined field name cannot contain a colon."));

        if (fieldName.Contains(Values.COMMA.ToString()))
          exceptions.Add(new Exception("User-defined field name cannot contain a comma."));

        if (validateTagNameMatch)
        {
          if (TagNames.IsTagName(fieldName))
            exceptions.Add(new Exception("User-defined field name cannot match the name of a standard ADIF field."));
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
  }
}
