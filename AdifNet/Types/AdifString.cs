
namespace org.goodspace.Data.Radio.Adif.Types {

  /// <summary>
  /// Represents the String ADIF type.
  /// </summary>
  public class AdifString : AdifType<string>, IAdifType {

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.String;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.String;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public override string Parse(string? s)
    {
      s ??= string.Empty;

      if (!s.IsAscii())
        throw new Exception("ADIF String cannot contain non-ASCII characters.");

      if (s.HasLineEnding())
        throw new Exception("ADIF String cannot contain line endings.");

      return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public override bool TryParse(string? s, out string? result)
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
    public override bool IsValidValue(object? o)
    {
      return IsValidValue(o == null ? string.Empty : o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public override bool IsValidValue(string? s)
    {
      s ??= string.Empty;
      return s.IsAscii() && 
             !s.HasLineEnding();
    }
  }
}
