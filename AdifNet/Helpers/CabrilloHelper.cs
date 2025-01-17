﻿
namespace org.goodspace.Data.Radio.Adif.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CabrilloHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly List<AdifModeGrouping> ModeGroupings;

        /// <summary>
        /// 
        /// </summary>
        static CabrilloHelper()
        {
            ModeGroupings = [.. AdifModeGrouping.GetAll()];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qso"></param>
        public static string? GetQsoMode(AdifQso qso)
        {
            var mode = qso.GetTagValue<string>(AdifTags.Mode);

            if (string.IsNullOrEmpty(mode))
            {
                var subMode = qso.GetTagValue<string>(AdifTags.SubMode);
                if (!string.IsNullOrEmpty(subMode) && AdifEnumerations.SubModes.IsValid(subMode))
                {
                    var parentMode = AdifEnumerations.SubModes.GetValue(subMode);
                    if (parentMode != null)
                        mode = parentMode.Code;
                }
            }

            if (string.IsNullOrEmpty(mode))
                throw new Exception("Cannot retrieve Cabrillo mode: no ADIF mode was found in the QSO.");

            return GetQsoMode(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adifMode"></param>
        public static string? GetQsoMode(string adifMode)
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
