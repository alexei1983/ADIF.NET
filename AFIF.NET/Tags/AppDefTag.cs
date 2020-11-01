using System;

namespace ADIF.NET.Tags {

  /// <summary>
  /// Represents an application-defined field and value.
  /// </summary>
  public class AppDefTag : Tag<object>, ITag {

    public override string Name => $"{TagNames.AppDef}{ProgramId ?? Values.DefaultProgramId}_{FieldName ?? string.Empty}";

    public string FieldName { get; set; }

    public string ProgramId {

      get {
        return programId;
        }

      set {
        if (value?.Contains(Values.Underscore.ToString()) ?? false)
          throw new ArgumentException("Program ID cannot contain an underscore.");

        programId = value;
        }
      }

    public string DataType { get; set; }

    public new void SetValue(object value) {    
      var convertedVal = ConvertValue(value);
      base.SetValue(convertedVal);
      }

    public override object ConvertValue(object value) {

      if (!(value is null)) {

        switch (DataType ?? DataTypes.String) {

        case DataTypes.Number:
          return value.ToDouble();

        case DataTypes.Time:
        case DataTypes.Date:
          return value.ToDateTime();

        case DataTypes.Boolean:
          return value.ToBoolean();

        default:
          return value.ToString() ?? string.Empty;
          }
        }

      return value;
      }

    string programId;

    }
  }
