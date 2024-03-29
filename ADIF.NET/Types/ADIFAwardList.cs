﻿using System;
using System.Collections.Generic;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the AwardList ADIF type.
  /// </summary>
  public class ADIFAwardList : ADIFType<string>, IADIFType {

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.AwardList;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.AwardList;

    /// <summary>
    /// Whether or not the type is multivalued.
    /// </summary>
    public override bool MultiValue => true;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string[] Parse(string s)
    {
      return ParseAwardList(s, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out string[] result)
    {
      try
      {
        result = ParseAwardList(s, true);
        return true;
      }
      catch
      {
        result = null;
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="o"></param>
    public static bool IsValidValue(object o)
    {
      if (o is null) 
        return true;

       return IsValidValue(o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool IsValidValue(string s)
    {
      if (string.IsNullOrEmpty(s))
        return true;

      try
      {
        ParseAwardList(s, true);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="throwExceptions"></param>
    static string[] ParseAwardList(string s, bool throwExceptions)
    {
      if (s == null)
        return null;

      var awards = Values.Awards;
      var result = new List<string>();
      var exceptions = new List<Exception>();

      // split by comma
      var split = s.Split(Values.COMMA);

      foreach (var award in split)
      {
        var checkedCount = 0;

        if (!awards.IsValid(award))
          checkedCount++;

        if (checkedCount >= awards.Count)
          exceptions.Add(new Exception($"Award '{award.ToUpper()}' is not valid."));
        else
          result.Add(award);
      }

      if (throwExceptions)
      {
        if (exceptions.Count > 1)
          throw new AggregateException(exceptions.ToArray());
        else if (exceptions.Count == 1)
          throw exceptions[0];
      }

      return result.ToArray();
    }
  }
}
