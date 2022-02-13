﻿
namespace ADIF.NET.Types {

  /// <summary>
  /// 
  /// </summary>
  public class ADIFMultilineString : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.MultilineString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      if (s == null)
        s = string.Empty;

      if (!s.IsASCII())
        throw new System.Exception("Invalid MultilineString.");

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
      return !string.IsNullOrEmpty(s) && s.IsASCII();
    }
  }
}