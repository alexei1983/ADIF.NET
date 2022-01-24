﻿using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents a user-defined QSO field.
  /// </summary>
  public class UserDefTag : StringTag, ITag {

    public override string Name => TagNames.UserDef;

    public string FieldName
    {
      get { return fieldName; }

      set
      {
        UserDefHelper.ValidateFieldName(value);
        fieldName = value;
      }
    }

    public override bool Header => true;

    public int FieldId { get; set; }

    public string[] CustomOptions { get; set; }

    public double LowerBound { get; set; }
    public double UpperBound { get; set; }

    public string DataType { get; set; }

    string fieldName;
  }
}
