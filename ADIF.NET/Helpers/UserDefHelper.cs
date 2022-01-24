using System;
using System.Collections.Generic;

namespace ADIF.NET.Helpers {
  public static class UserDefHelper {

    /// <summary>
    /// Validates the specified user-defined field name.
    /// </summary>
    /// <param name="fieldName">Name of the user-defined field to validate.</param>
    /// <param name="throwExceptions">Whether or not validation exceptions will be thrown.</param>
    public static bool ValidateFieldName(string fieldName, bool throwExceptions = true)
    {
      var exceptions = new List<Exception>();

      if (string.IsNullOrWhiteSpace(fieldName))
      {
        exceptions.Add(new Exception("User-defined field name cannot be null, empty string, or white space."));    
      } else
      { 
        if (fieldName[0] == ' ' || fieldName[fieldName.Length - 1] == ' ')
          exceptions.Add(new Exception("User-defined field name cannot begin or end with a space."));

        if (fieldName.Contains("{") || fieldName.Contains("}"))
          exceptions.Add(new Exception("User-defined field name cannot contain curly braces."));

        if (fieldName.Contains("<") || fieldName.Contains(">"))
          exceptions.Add(new Exception("User-defined field name cannot contain angle brackets (greater-than or less-than sign)."));

        if (fieldName.Contains(":"))
          exceptions.Add(new Exception("User-defined field name cannot contain a colon."));

        if (fieldName.Contains(","))
          exceptions.Add(new Exception("User-defined field name cannot contain a comma."));

        if (TagNames.IsTagName(fieldName))
          exceptions.Add(new Exception("User-defined field name cannot match the name of a standard ADIF field."));
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
