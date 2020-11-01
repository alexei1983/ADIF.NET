using System;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public static class ValueStore {

    /// <summary>
    /// 
    /// </summary>
    static ValueStore() {

      CountryCodes = CountryCode.Get()?.ToList();
      ArrlSections = ArrlSection.Get()?.ToList();

      }

    /// <summary>
    /// 
    /// </summary>
    public static List<CountryCode> CountryCodes { get; }

    /// <summary>
    /// 
    /// </summary>
    public static List<Option> ArrlSections { get; }


    }
  }
