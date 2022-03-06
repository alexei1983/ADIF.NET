using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the IntlString ADIF type.
  /// </summary>
  public class ADIFIntlString : ADIFType<string>, IADIFType {

    /// <summary>
    /// The ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.IntlString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      if (s == null)
        s = string.Empty;

      if (s.Contains(Environment.NewLine) || s.Contains(Values.LINE_ENDING.ToString()))
        throw new Exception("ADIF IntlString cannot contain line endings.");

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

      return !s.Contains(Environment.NewLine) &&
             !s.Contains(Values.LINE_ENDING.ToString());
    }
  }
}
