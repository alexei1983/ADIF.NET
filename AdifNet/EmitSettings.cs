﻿
namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Special instructions for generating ADIF or ADX.
    /// </summary>
    [Flags]
    public enum EmitFlags
    {
        /// <summary>
        /// No special option(s) set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Force tag names to be lowercase in generated ADIF.
        /// </summary>
        LowercaseTagNames = 2,

        /// <summary>
        /// Automatically add the CREATED_TIMESTAMP header tag if it is not already present.
        /// </summary>
        AddCreatedTimestamp = 4,

        /// <summary>
        /// Automatically add the PROGRAMID and PROGRAMVERSION header tags if they are not already present.
        /// </summary>
        AddProgramHeaderTags = 8,

        /// <summary>
        /// Set the value of STATION_CALLSIGN to the value of OPERATOR or vice versa.
        /// </summary>
        MirrorOperatorAndStationCallSign = 16,
    }
}
