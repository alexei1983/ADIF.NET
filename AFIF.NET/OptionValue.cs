using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Attributes;
using ADIF.NET.Helpers;

namespace ADIF.NET {

  public class OptionValue {

    public string Value { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string[] AlternateNames { get; set; }

    public string[] SubOptions { get; set; }

    public bool Legacy { get; set; }

    public bool ImportOnly { get; set; }

    public OptionValue() { }

    public OptionValue(string name, string value) : this(name, name, value, false, false, null) {
      }

    public OptionValue(string name, 
                       string displayName,
                       string value,
                       bool legacy,
                       bool importOnly,
                       params string[] alternateNames) {
      Name = name;
      Value = value;
      DisplayName = displayName;
      Legacy = legacy;
      ImportOnly = importOnly;
      AlternateNames = alternateNames;
      }

    public static IEnumerable<OptionValue> FromDatabase(string databasePath, 
                                                        string query) {

      using (var dbHelper = new SQLiteHelper(databasePath)) { 

        if (dbHelper.Ready) { 

          var results = dbHelper.ReadData(query);

          foreach (dynamic result in results) {

            yield return new OptionValue() { Name = $"{result.Value} - {result.Name}",  
                                             Value = result.Value?.ToString(),
                                             Legacy = result.Legacy,
                                             ImportOnly = result.ImportOnly,
                                             DisplayName = result.Name?.ToString() };
            }
          }
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="excludeImportOnly"></param>
    /// <param name="excludeLegacy"></param>
    /// <returns></returns>
    public static IEnumerable<OptionValue> FromType(Type type, 
                                                    bool excludeImportOnly = false,
                                                    bool excludeLegacy = false) {

      var constants = type.GetConstants();
      
      foreach (var constant in constants) {

        var importOnly = constant.GetAttributeValue<ImportOnlyAttribute, bool>((a) => { return a.ImportOnly; });

        if (excludeImportOnly && importOnly)
          continue;

        var legacy = constant.GetAttributeValue<LegacyValueAttribute, bool>((a) => { return a.Legacy; });

        if (excludeLegacy && legacy)
          continue;

        var alternateNames = constant.GetAttributeValues<AlternateNameAttribute, string>((a) => { return a.Name; });
        var displayName = constant.GetAttributeValue<DisplayNameAttribute, string>((a) => { return a.Name; });
        var subOptions = constant.GetSubOptions<SubOptionAttribute>((a) => { return a.SubOption; });

        yield return new OptionValue() { Name = constant.Name,
                                         Value = constant.GetRawConstantValue().ToString(),
                                         DisplayName = string.IsNullOrWhiteSpace(displayName) ? constant.Name : displayName,
                                         AlternateNames = alternateNames?.ToArray() ?? new string[] { },
                                         SubOptions = subOptions?.ToArray() ?? new string[] { },
                                         Legacy = legacy,
                                         ImportOnly = importOnly }; 

        }
      }
    }
  }
