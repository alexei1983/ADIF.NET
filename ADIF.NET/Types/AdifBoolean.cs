﻿using System;

namespace ADIF.NET.Types {

  /// <summary>
  /// Represents the Boolean ADIF type.
  /// </summary>
  public class ADIFBoolean : ADIFType<bool?> {

    /// <summary>
    /// Whether or not the type is an enumeration.
    /// </summary>
    public override bool IsEnumeration => true;

    /// <summary>
    /// ADIF data type indicator.
    /// </summary>
    public override string Type => DataTypes.Boolean;

    /// <summary>
    /// ADIF data type name.
    /// </summary>
    public override string TypeName => DataTypeNames.Boolean;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public static bool? Parse(string s)
    {
      if (!FromString(s, out bool? result))
        throw new ArgumentException($"Invalid ADIF Boolean value: '{s ?? string.Empty}'");

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    public static bool TryParse(string s, out bool? result)
    {
      return FromString(s, out result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(object value)
    {
      if (value is bool || value is bool?)
        return true;

      return FromString(value == null ? string.Empty : value.ToString(), out bool? _);  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public static bool IsValidValue(string value)
    {
      return FromString(value ?? string.Empty, out bool? _);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    static bool FromString(string s, out bool? result)
    {
      var success = false;
      result = null;

      s = (s ?? string.Empty).ToUpper();

      switch (s)
      {
        case Values.ADIF_BOOLEAN_TRUE:
          result = true;
          success = true;
          break;

        case Values.ADIF_BOOLEAN_FALSE:
          result = false;
          success = true;
          break;

        case "":
          result = null;
          success = true;
          break;

        default:
          success = false;
          break;
      }

      return success;
    }
  }
}
