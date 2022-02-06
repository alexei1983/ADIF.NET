using System;
using ADIF.NET.Helpers;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an application-defined field and value.
  /// </summary>
  public class AppDefTag : Tag<object>, ITag, ICloneable {

    public override string Name => $"{TagNames.AppDef}{ProgramId ?? Values.DEFAULT_PROGRAM_ID}_{FieldName ?? string.Empty}";

    public string FieldName { get; set; }

    public string ProgramId {

      get {
        return programId;
        }

      set {
        if (value != null && value.Contains(Values.UNDERSCORE.ToString()))
          throw new ArgumentException("Program ID cannot contain an underscore.");

        programId = value;
        }
      }

    public new string DataType { get; set; }

    public new void SetValue(object value)
    {
      var convertedVal = ConvertValue(value);
      base.SetValue(convertedVal);
    }

    public override object ConvertValue(object value)
    {
      return !(value is null) ? UserDefHelper.ConvertValueByType(value, DataType) : null;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    string programId;
    }
  }
