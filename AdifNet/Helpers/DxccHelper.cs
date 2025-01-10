using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Helpers
{

    /// <summary>
    /// Helper methods for working with DXCC entities and their primary and secondary administrative subdivisions.
    /// </summary>
    public static class DxccHelper
    {

        const string VALIDATE_PRI_SUB_SQL = "SELECT 1 FROM CountryCodes c INNER JOIN PrimaryAdminSubdivisions p ON c.Code = p.CountryCode WHERE c.Code = @DXCC AND p.Code = @SubdivisionCode";
        const string VALIDATE_DXCC_HAS_PRI_SUB_SQL = "SELECT COUNT(Code) AS DXCCCount FROM PrimaryAdminSubdivisions WHERE CountryCode = @DXCC";
        const string VALIDATE_DXCC_HAS_SEC_SUB_SQL = "SELECT COUNT(Code) AS DXCCCount FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC";
        const string VALIDATE_PRI_SUB_HAS_SEC_SUB_SQL = "SELECT COUNT(Code) AS SecondaryCount FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC AND PrimarySubdivisionCode = @PrimaryCode";
        const string VALIDATE_SEC_SUB_SQL = "SELECT 1 FROM SecondaryAdminSubdivisions WHERE CountryCode = @DXCC AND (PrimarySubdivisionCode = @PrimaryCode OR PrimarySubdivisionCode IS NULL) AND Code = @SecondaryCode";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dxcc"></param>
        /// <param name="primaryAdminSubdivisionCode"></param>
        public static bool ValidatePrimarySubdivision(int dxcc, string primaryAdminSubdivisionCode)
        {
            if (dxcc < 1 || string.IsNullOrEmpty(primaryAdminSubdivisionCode))
                throw new AdministrativeSubdivisionException("Cannot validate primary administrative subdivision code: missing required parameter(s).");

            if (!CountryHasPrimarySubdivision(dxcc))
                return true;

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_PRI_SUB_SQL,
                                                                   new Dictionary<string, object?>() { { "@DXCC", dxcc },
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
                throw new DxccException("Invalid DXCC entity.", dxcc.ToString());

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_DXCC_HAS_PRI_SUB_SQL,
                                                                   new Dictionary<string, object?>() { { "@DXCC", dxcc } });

            return result > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dxcc"></param>
        public static bool CountryHasSecondarySubdivision(int dxcc)
        {
            if (dxcc < 1)
                throw new DxccException("Invalid DXCC entity.", dxcc.ToString());

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_DXCC_HAS_SEC_SUB_SQL,
                                                                   new Dictionary<string, object?>() { { "@DXCC", dxcc } });

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
                throw new AdministrativeSubdivisionException("Missing required parameter(s).");

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_PRI_SUB_HAS_SEC_SUB_SQL,
                                                                   new Dictionary<string, object?>() { { "@DXCC", dxcc },
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
                throw new AdministrativeSubdivisionException("Cannot validate secondary administrative subdivision code: missing required parameter(s).");

            if (!PrimarySubdivisionHasSecondarySubdivision(dxcc, primaryAdminSubdivisionCode))
                return true;

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(VALIDATE_SEC_SUB_SQL,
                                                                   new Dictionary<string, object?>() { { "@DXCC", dxcc },
                                                                                                { "@PrimaryCode", primaryAdminSubdivisionCode },
                                                                                                { "@SecondaryCode", secondaryAdminSubdivisionCode }});

            return result == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public static int ConvertDxcc(string code)
        {
            if (!int.TryParse(code, out int dxcc))
                throw new DxccException("Invalid DXCC entity.", code);

            if (dxcc < 1)
                throw new DxccException("Invalid DXCC entity.", code);

            return dxcc;
        }
    }
}
