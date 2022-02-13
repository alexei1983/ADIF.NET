using System;
using System.Collections.Generic;

namespace ADIF.NET.Helpers {

  /// <summary>
  /// Helper methods for working with DXCC entities and their primary and secondary administrative subdivisions.
  /// </summary>
  public static class DXCCHelper {

    const string VALIDATE_PRI_SUB_SQL = "SELECT 1 FROM CountryCodes c INNER JOIN PrimaryAdminSubdivisions p ON c.Code = p.CountryCode WHERE c.Code = @DXCC AND p.Code = @SubdivisionCode";
    const string VALIDATE_DXCC_HAS_PRI_SUB_SQL = "SELECT COUNT(Code) AS DXCCCount FROM PrimaryAdminSubdivisions WHERE CountryCode = @DXCC";
    const string VALIDATE_DXCC_HAS_SEC_SUB_SQL = "SELECT COUNT(Code) AS DXCCCount FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC";
    const string VALIDATE_PRI_SUB_HAS_SEC_SUB_SQL = "SELECT COUNT(Code) AS SecondaryCount FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC AND PrimarySubdivisionCode = @PrimaryCode";
    const string VALIDATE_SEC_SUB_SQL = "SELECT 1 FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC AND (PrimarySubdivisionCode = @PrimaryCode OR PrimarySubdivisionCode IS NULL) AND Code = @SecondaryCode";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxccCode"></param>
    /// <param name="primaryAdminSubdivisionCode"></param>
    public static bool ValidatePrimarySubdivision(int dxcc, string primaryAdminSubdivisionCode)
    {
      if (dxcc < 1 || string.IsNullOrEmpty(primaryAdminSubdivisionCode))
        throw new ArgumentException("Cannot validate primary administrative subdivision code: missing required parameter(s).");

      if (!CountryHasPrimarySubdivision(dxcc))
        return true;

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_PRI_SUB_SQL, 
                                                             new Dictionary<string, object>() { { "@DXCC", dxcc },
                                                                                                { "@SubdivisionCode", primaryAdminSubdivisionCode } });
                                                                                                   
      return result == 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxcc"></param>
    public static bool CountryHasPrimarySubdivision(int dxcc)
    {
      if (dxcc < 1)
        throw new ArgumentException("Invalid DXCC entity.");

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_DXCC_HAS_PRI_SUB_SQL,
                                                             new Dictionary<string, object>() { { "@DXCC", dxcc } });

      return result > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxcc"></param>
    public static bool CountryHasSecondarySubdivision(int dxcc)
    {
      if (dxcc < 1)
        throw new ArgumentException("Invalid DXCC entity.");

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_DXCC_HAS_SEC_SUB_SQL,
                                                             new Dictionary<string, object>() { { "@DXCC", dxcc } });

      return result > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxcc"></param>
    /// <param name="primaryAdminSubdivisionCode"></param>
    public static bool PrimarySubdivisionHasSecondarySubdivision(int dxcc,
                                                                 string primaryAdminSubdivisionCode)
    {
      if (dxcc < 1 || string.IsNullOrEmpty(primaryAdminSubdivisionCode))
        throw new ArgumentException("Missing required parameter(s).");

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_PRI_SUB_HAS_SEC_SUB_SQL,
                                                             new Dictionary<string, object>() { { "@DXCC", dxcc },
                                                               { "@PrimaryCode", primaryAdminSubdivisionCode } });

      return result > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxcc"></param>
    /// <param name="primaryAdminSubdivisionCode"></param>
    /// <param name="secondaryAdminSubdivisionCode"></param>
    public static bool ValidateSecondarySubdivision(int dxcc, string primaryAdminSubdivisionCode, string secondaryAdminSubdivisionCode)
    {
      if (dxcc < 1 || string.IsNullOrEmpty(primaryAdminSubdivisionCode) || string.IsNullOrEmpty(secondaryAdminSubdivisionCode))
        throw new ArgumentException("Cannot validate secondary administrative subdivision code: missing required parameter(s).");

      if (!PrimarySubdivisionHasSecondarySubdivision(dxcc, primaryAdminSubdivisionCode))
        return true;

      var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_SEC_SUB_SQL,
                                                             new Dictionary<string, object>() { { "@DXCC", dxcc },
                                                                                                { "@PrimaryCode", primaryAdminSubdivisionCode },
                                                                                                { "@SecondaryCode", secondaryAdminSubdivisionCode }});

      return result == 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dxccCode"></param>
    public static int ConvertDXCC(string dxccCode)
    {
      if (!int.TryParse(dxccCode, out int dxcc))
        throw new Exception("Invalid DXCC entity.");

      if (dxcc < 1)
        throw new Exception("Invalid DXCC entity.");

      return dxcc;
    }
  }
}
