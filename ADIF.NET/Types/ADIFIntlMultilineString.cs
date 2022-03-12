
namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the IntlMultilineString ADIF type.
  /// </summary>
  public class ADIFIntlMultilineString : ADIFType<string>, IADIFType {

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.IntlMultilineString;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.IntlMultilineString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static string Parse(string s)
    {
      return s ?? string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out string result)
    {
      result = s ?? string.Empty;
      return true;
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
      return true;
    }
  }
}
