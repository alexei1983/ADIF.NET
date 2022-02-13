using System;

namespace ADIF.NET.Types {

  public class ADIFGridSquare : ADIFType<string>, IADIFType {

    public override string Type => string.Empty;

    public static string Parse(string s)
    {
      if (IsValidValue(s))
        return s;

      throw new Exception($"Invalid GridSquare: '{s}'");
    }

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

    public static bool IsValidValue(object o)
    {
      if (o is null)
        return false;

      return IsValidValue(o.ToString());
    }

    public static bool IsValidValue(string s)
    {
      if (string.IsNullOrEmpty(s))
        return false;

      var len = s.Length;

      if (len != 2 && len != 4 && len != 6 && len != 8)
        return false;

      return true;
    }
  }
}
