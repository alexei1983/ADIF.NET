﻿using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFString : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.String;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      if (s == null)
        s = string.Empty;

      if (!s.IsASCII())
        throw new Exception("Invalid ADIF String.");

      if (s.Contains(Environment.NewLine) || s.Contains(Values.LINE_ENDING.ToString()))
        throw new Exception("ADIF String cannot contain line endings.");

      return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out string result)
    {
      result = null;
      try
      {
        result = Parse(s);
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
    /// <param name="o"></param>
    public bool IsValidValue(object o)
    {
      return IsValidValue(o == null ? string.Empty : o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public bool IsValidValue(string s)
    {
      return !string.IsNullOrEmpty(s) && 
             s.IsASCII() && 
             !s.Contains(Environment.NewLine) && 
             !s.Contains(Values.LINE_ENDING.ToString());
    }
  }
}
