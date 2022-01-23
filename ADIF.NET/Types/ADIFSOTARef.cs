using System;

namespace ADIF.NET.Types {
  public class ADIFSOTARef : ADIFType<string>, IADIFType {

    public static string Parse(string s)
    {
      if (IsValidValue(s))
        return s;

      throw new Exception($"Invalid SOTARef: '{s}'");
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

      return s.IsSOTADesignator();
    }
  }
}
