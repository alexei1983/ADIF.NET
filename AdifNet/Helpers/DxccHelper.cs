using org.goodspace.Data.Radio.Adif.Exceptions;

namespace org.goodspace.Data.Radio.Adif.Helpers
{
    /// <summary>
    /// Helper methods for working with DXCC entities and their primary and secondary administrative subdivisions.
    /// </summary>
    internal static class DxccHelper
    {
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

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(Resources.SqlValidatePrimarySubdivision,
                                                                   new Dictionary<string, object?>() { { Resources.SqlParameterCountryCode, dxcc },
                                                                                                       { Resources.SqlParameterSubdivisionCode, primaryAdminSubdivisionCode } });

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

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(Resources.SqlValidateDxccHasPrimarySubdivisions,
                                                                   new Dictionary<string, object?>() { { Resources.SqlParameterCountryCode, dxcc } });

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

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(Resources.SqlValidateDxccHasSecondarySubdivisions,
                                                                   new Dictionary<string, object?>() { { Resources.SqlParameterCountryCode, dxcc } });

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

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(Resources.SqlValidatePrimarySubdivisionHasSecondarySubdivisions,
                                                                   new Dictionary<string, object?>() { { Resources.SqlParameterCountryCode, dxcc },
                                                                                                       { Resources.SqlParameterSubdivisionCode, primaryAdminSubdivisionCode } });

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

            var result = SQLiteHelper.Instance.ExecuteScalar<long>(Resources.SqlValidateSecondarySubdivision,
                                                                   new Dictionary<string, object?>() { { Resources.SqlParameterCountryCode, dxcc },
                                                                                                       { Resources.SqlParameterParent, primaryAdminSubdivisionCode },
                                                                                                       { Resources.SqlParameterSubdivisionCode, secondaryAdminSubdivisionCode }});

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
