using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field.
  /// </summary>
  public class UserDefTag : StringTag, ITag {

    /// <summary>
    /// 
    /// </summary>
    public override string Name => TagNames.UserDef;

    /// <summary>
    /// 
    /// </summary>
    public string FieldName
    {
      get { return fieldName; }

      set
      {
        UserDefHelper.ValidateFieldName(value);
        fieldName = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public override bool Header => true;

    /// <summary>
    /// 
    /// </summary>
    public override bool IsUserDef => true;

    /// <summary>
    /// 
    /// </summary>
    public new string DataType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int FieldId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] CustomOptions { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double LowerBound { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double UpperBound { get; set; }

    string fieldName;
  }
}
