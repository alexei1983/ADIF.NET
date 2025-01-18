using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AdifEnumerations
    {
        /// <summary>
        /// QSO upload status enumeration.
        /// </summary>
        internal static readonly AdifEnumeration QsoUploadStatuses;

        /// <summary>
        /// QSO download status enumeration.
        /// </summary>
        internal static readonly AdifEnumeration QsoDownloadStatuses;

        /// <summary>
        /// QSO completion status enumeration.
        /// </summary>
        internal static readonly AdifEnumeration QsoCompleteStatuses;

        /// <summary>
        /// QSL via enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Via;

        /// <summary>
        /// Antenna path enumeration.
        /// </summary>
        internal static readonly AdifEnumeration AntennaPaths;

        /// <summary>
        /// QSL medium enumeration.
        /// </summary>
        internal static readonly AdifEnumeration QslMediums;

        /// <summary>
        /// Continent enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Continents;

        /// <summary>
        /// eSQL sent status enumeration.
        /// </summary>
        internal static readonly AdifEnumeration EQslSentStatuses;

        /// <summary>
        /// eQSL received status enumeration.
        /// </summary>
        internal static readonly AdifEnumeration EQslReceivedStatuses;

        /// <summary>
        /// Propagation mode enumeration.
        /// </summary>
        internal static readonly AdifEnumeration PropagationModes;

        /// <summary>
        /// ARRL section enumeration.
        /// </summary>
        internal static readonly AdifEnumeration ArrlSections;

        /// <summary>
        /// Morse key type enumeration.
        /// </summary>
        internal static readonly AdifEnumeration MorseKeyTypes;

        /// <summary>
        /// Award enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Awards;

        /// <summary>
        /// Mode enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Modes;

        /// <summary>
        /// Sub-mode enumeration.
        /// </summary>
        internal static readonly AdifEnumeration SubModes;

        /// <summary>
        /// Sponsored award prefix enumeration.
        /// </summary>
        internal static readonly AdifEnumeration SponsoredAwardPrefixes;

        /// <summary>
        /// Country code enumeration.
        /// </summary>
        internal static readonly AdifEnumeration CountryCodes;

        /// <summary>
        /// Contest enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Contests;

        /// <summary>
        /// Amateur radio band enumeration
        /// </summary>
        internal static readonly AdifEnumeration Bands;

        /// <summary>
        /// Credit enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Credits;

        /// <summary>
        /// QSL received statuses.
        /// </summary>
        internal static readonly AdifEnumeration QslReceivedStatuses;

        /// <summary>
        /// QSL sent statuses.
        /// </summary>
        internal static readonly AdifEnumeration QslSentStatuses;

        /// <summary>
        /// ADIF boolean values.
        /// </summary>
        internal static readonly AdifEnumeration BooleanValues;

        /// <summary>
        /// DARC DOK enumeration.
        /// </summary>
        internal static readonly AdifEnumeration DarcDoks;

        /// <summary>
        /// Primary administrative subdivisions.
        /// </summary>
        internal static readonly AdifEnumeration PrimarySubdivisions;

        /// <summary>
        /// Secondary administrative subdivisions.
        /// </summary>
        internal static readonly AdifEnumeration SecondarySubdivisions;

        /// <summary>
        /// Alternate secondary administrative subdivisions.
        /// </summary>
        internal static readonly AdifEnumeration SecondarySubdivisionAlts;

        /// <summary>
        /// Regions enumeration.
        /// </summary>
        internal static readonly AdifEnumeration Regions;

        /// <summary>
        /// ARRL precedence enumeration.
        /// </summary>
        internal static readonly AdifEnumeration ArrlPrecedence;

        /// <summary>
        /// Instantiates the static data fields for the class.
        /// </summary>
        static AdifEnumerations()
        {
            QsoUploadStatuses = AdifEnumeration.Get(Resources.EnumNameQsoUploadStatus) ?? [];
            QsoCompleteStatuses = AdifEnumeration.Get(Resources.EnumNameQsoCompleteStatus) ?? [];
            Via = AdifEnumeration.Get(Resources.EnumNameVia) ?? [];
            AntennaPaths = AdifEnumeration.Get(Resources.EnumNameAntennaPath) ?? [];
            QslMediums = AdifEnumeration.Get(Resources.EnumNameQslMedium) ?? [];
            Continents = AdifEnumeration.Get(Resources.EnumNameContinent) ?? [];
            EQslSentStatuses = AdifEnumeration.Get(Resources.EnumNameEQslSentStatus) ?? [];
            EQslReceivedStatuses = AdifEnumeration.Get(Resources.EnumNameEQslReceivedStatus) ?? [];
            PropagationModes = AdifEnumeration.Get(Resources.EnumNamePropagationMode) ?? [];
            ArrlSections = AdifEnumeration.Get(Resources.EnumNameArrlSection) ?? [];
            Awards = AdifEnumeration.Get(Resources.EnumNameAward) ?? [];
            Modes = AdifEnumeration.Get(Resources.EnumNameMode) ?? [];
            SubModes = AdifEnumeration.Get(Resources.EnumNameSubMode) ?? [];
            SponsoredAwardPrefixes = AdifEnumeration.Get(Resources.EnumNameSponsoredAwardPrefix) ?? [];
            CountryCodes = AdifEnumeration.Get(Resources.EnumNameDxcc) ?? [];
            Contests = AdifEnumeration.Get(Resources.EnumNameContestId) ?? [];
            Bands = AdifEnumeration.Get(Resources.EnumNameBand) ?? [];
            Credits = AdifEnumeration.Get(Resources.EnumNameCredit) ?? [];
            QslSentStatuses = AdifEnumeration.Get(Resources.EnumNameQslSent) ?? [];
            QslReceivedStatuses = AdifEnumeration.Get(Resources.EnumNameQslRcvd) ?? [];
            BooleanValues = AdifEnumeration.Get(nameof(AdifBoolean)) ?? [];
            DarcDoks = AdifEnumeration.Get(Resources.EnumNameDarcDok) ?? [];
            Regions = AdifEnumeration.Get(Resources.EnumNameRegion) ?? [];
            PrimarySubdivisions = AdifEnumeration.Get(Resources.EnumNamePrimarySubdivision) ?? [];
            SecondarySubdivisions = AdifEnumeration.Get(Resources.EnumNameSecondarySubdivision) ?? [];
            ArrlPrecedence = AdifEnumeration.Get(Resources.EnumNameArrlPrecedence) ?? [];
            MorseKeyTypes = AdifEnumeration.Get(Resources.EnumNameMorseKeyType) ?? [];
            QsoDownloadStatuses = AdifEnumeration.Get(Resources.EnumNameQsoDownloadStatus) ?? [];
            SecondarySubdivisionAlts = AdifEnumeration.Get(Resources.EnumNameSecondarySubdivisionAlt) ?? [];
        }
    }
}
