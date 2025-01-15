
namespace org.goodspace.Data.Radio.Adif.Types {

  /// <summary>
  /// Represents the SOTARef ADIF type.
  /// </summary>
  public class AdifSotaRef : AdifType<string>, IAdifType {

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.SotaRef;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public override string Parse(string? s)
    {
      if (IsValidValue(s))
        return s ?? string.Empty;

      throw new Exception($"Invalid SOTARef: '{s}'");
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
      return IsValidValue(o is null ? string.Empty : o.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public override bool IsValidValue(string? s)
    {
      return string.IsNullOrWhiteSpace(s) || s.IsSotaDesignator();
    }
  }
}
