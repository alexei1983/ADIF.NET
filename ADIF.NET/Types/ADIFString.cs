using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the String ADIF type.
  /// </summary>
  public class ADIFString : ADIFType<string>, IADIFType {

    /// <summary>
    /// ADIF data type indicator.
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
        throw new Exception("ADIF String cannot contain non-ASCII characters.");

      if (s.Contains(Environment.NewLine) || s.Contains(Values.NEWLINE.ToString()) || s.Contains(Values.CARRIAGE_RETURN.ToString()))
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

      return s.IsASCII() && 
             !s.Contains(Environment.NewLine) && 
             !s.Contains(Values.NEWLINE.ToString()) &&
             !s.Contains(Values.CARRIAGE_RETURN.ToString());
    }
  }
}
