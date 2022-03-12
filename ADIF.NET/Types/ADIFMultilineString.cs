using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the MultilineString ADIF type.
  /// </summary>
  public class ADIFMultilineString : ADIFType<string>, IADIFType {

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.MultilineString;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.MultilineString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      if (s == null)
        s = string.Empty;

      if (!s.IsASCII())
        throw new Exception("Invalid MultilineString.");

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
      if (s == null)
        s = string.Empty;

      return s.IsASCII();
    }
  }
}
