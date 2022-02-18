using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the GridSquare ADIF type.
  /// </summary>
  public class ADIFGridSquare : ADIFType<string>, IADIFType {

    /// <summary>
    /// 
    /// </summary>
    public override string Type => string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      if (IsValidValue(s))
        return s;

      throw new Exception($"Invalid GridSquare: '{s}'");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out string result)
    {
      try
      {
        result = Parse(s);
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
      return IsValidValue(o is null ? string.Empty : o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool IsValidValue(string s)
    {
      if (s == null)
        s = string.Empty;

      var len = s.Length;

      if (len != 0 && len != 2 && len != 4 && len != 6 && len != 8)
        return false;

      return true;
    }
  }
}
