using org.goodspace.Data.Radio.Adif.Helpers;
using System.Dynamic;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Represents a grouping of ADIF modes.
    /// </summary>
    public class AdifModeGrouping
    {
        /// <summary>
        /// ADIF mode.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Cabrillo mode.
        /// </summary>
        public string CabrilloMode { get; set; }

        /// <summary>
        /// Group name.
        /// </summary>
        public string Grouping { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public AdifModeGrouping(dynamic value)
        {
            if (value is IDictionary<string, object?> dict)
            {
                if (dict.TryGetValue(nameof(Mode), out object? mode) && mode is string _mode)
                    Mode = _mode;

                if (dict.TryGetValue(nameof(CabrilloMode), out object? cabrilloMode) && cabrilloMode is string _cabrilloMode)
                    CabrilloMode = _cabrilloMode;

                if (dict.TryGetValue(nameof(Grouping), out object? grouping) && grouping is string _grouping)
                    Grouping = _grouping;
            }
            Mode ??= string.Empty;
            CabrilloMode ??= string.Empty;
            Grouping ??= string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<AdifModeGrouping> GetAll()
        {
            var sqlResult = SQLiteHelper.Instance.ReadData(GET_MODE_GROUPING_SQL);

            if (sqlResult != null)
            {
                foreach (var result in sqlResult)
                {
                    var grouping = new AdifModeGrouping(result);
                    if (grouping != null && !string.IsNullOrEmpty(grouping.Mode))
                        yield return grouping;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        public static AdifModeGrouping Get(string mode)
        {
            if (string.IsNullOrEmpty(mode))
                throw new ArgumentException("Mode is required.", nameof(mode));

            var sqlResult = SQLiteHelper.Instance.ReadData(GET_MODE_GROUPING_BY_MODE_SQL,
                                                           new Dictionary<string, object?>() { { "@Mode", mode } });

            if (sqlResult != null && sqlResult.Count > 0)
            {
                if (sqlResult[0] is ExpandoObject dynamicValue)
                    return new AdifModeGrouping(dynamicValue);
            }

            throw new Exception($"No grouping found for mode {mode.ToUpper()}.");
        }

        const string GET_MODE_GROUPING_SQL = "SELECT \"Mode\", \"CabrilloMode\", \"Grouping\" FROM \"ModeGrouping\"";
        const string GET_MODE_GROUPING_BY_MODE_SQL = GET_MODE_GROUPING_SQL + " WHERE \"Mode\" = @Mode";
    }
}
