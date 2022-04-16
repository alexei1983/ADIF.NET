using System;
using System.Collections.Generic;
using System.Linq;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// 
  /// </summary>
  public static class CabrilloHelper {

    /// <summary>
    /// 
    /// </summary>
    public static readonly List<ModeGrouping> ModeGroupings;

    /// <summary>
    /// 
    /// </summary>
    static CabrilloHelper()
    {
      ModeGroupings = ModeGrouping.GetAll().ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qso"></param>
    public static string GetQSOMode(ADIFQSO qso)
    {
      var mode = qso.GetTagValue<string>(ADIFTags.Mode);

      if (string.IsNullOrEmpty(mode))
      {
        var submode = qso.GetTagValue<string>(ADIFTags.Submode);
        if (!string.IsNullOrEmpty(submode) && Values.Submodes.IsValid(submode))
        {
          var parentMode = Values.Submodes.GetValue(submode);
          if (parentMode != null)
            mode = parentMode.Code;
        }
      }

      if (string.IsNullOrEmpty(mode))
        throw new Exception("Cannot retrieve Cabrillo mode: no ADIF mode was found in the QSO.");

      return GetQSOMode(mode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adifMode"></param>
    public static string GetQSOMode(string adifMode)
    {
      if (string.IsNullOrEmpty(adifMode))
        throw new ArgumentException("ADIF mode is required.", nameof(adifMode));

      var grouping = ModeGroupings.FirstOrDefault(g => g.Mode.Equals(adifMode, StringComparison.OrdinalIgnoreCase));
      if (grouping != null)
        return grouping.CabrilloMode;

      return null;
    }
  }
}
