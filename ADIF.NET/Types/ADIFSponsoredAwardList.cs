using System;
using System.Collections.Generic;
using ADIF.NET.Exceptions;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the SponsoredAwardList ADIF type.
  /// </summary>
  public class ADIFSponsoredAwardList : ADIFType<string>, IADIFType {

    /// <summary>
    /// 
    /// </summary>
    public override string Type => DataTypes.SponsoredAwardList;

    /// <summary>
    /// 
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
        return false;

       return IsValidValue(o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool IsValidValue(string s)
    {
      if (string.IsNullOrEmpty(s))
        return false;

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
    static string[] ParseAwardList(string s, bool throwExceptions)
    {
      if (s == null)
        return null;

      var prefixes = Values.SponsoredAwardPrefixes.GetValues();
      var result = new List<string>();
      var exceptions = new List<Exception>();

      // split by comma
      var split = s.Split(Values.COMMA);

      foreach (var award in split)
      {
        var checkedCount = 0;

        foreach (var prefix in prefixes)
        {
          if (!award.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            checkedCount++;
        }

        if (checkedCount >= prefixes.Length)
          exceptions.Add(new SponsoredAwardListException($"Award '{award.ToUpper()}' does not have a valid sponsored prefix.", award.ToUpper()));
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
