﻿using System.Dynamic;
using System.Globalization;
using System.Reflection;
using org.goodspace.Data.Radio.Adif.Attributes;
using org.goodspace.Data.Radio.Adif.Helpers;
using org.goodspace.Data.Radio.Adif.Tags;
using org.goodspace.Data.Radio.Adif.Types;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// 
    /// </summary>
    public static class Values
    {
        /// <summary>
        /// ADIF date format.
        /// </summary>
        public const string ADIF_DATE_FORMAT = "yyyyMMdd";

        /// <summary>
        /// Long ADIF time format.
        /// </summary>
        public const string ADIF_TIME_FORMAT_LONG = "HHmmss";

        /// <summary>
        /// Short ADIF time format.
        /// </summary>
        public const string ADIF_TIME_FORMAT_SHORT = "HHmm";

        /// <summary>
        /// Character that represents the opening of an ADIF tag.
        /// </summary>
        public const char TAG_OPENING = '<';

        /// <summary>
        /// Character that represents the closing of an ADIF tag.
        /// </summary>
        public const char TAG_CLOSING = '>';

        /// <summary>
        /// Character that represents the start of a comment in an ADIF file.
        /// </summary>
        public const char COMMENT_INDICATOR = '#';

        /// <summary>
        /// Newline character.
        /// </summary>
        public const char NEWLINE = '\n';

        /// <summary>
        /// Carriage return character.
        /// </summary>
        public const char CARRIAGE_RETURN = '\r';

        /// <summary>
        /// Ampersand character.
        /// </summary>
        public const char AMPERSAND = '&';

        /// <summary>
        /// Default character to delimit list items in ADIF.
        /// </summary>
        public const char LIST_DELIMITER = COLON;

        /// <summary>
        /// Character that indicates the start of an ADIF tag length.
        /// </summary>
        public const char VALUE_LENGTH_CHAR = COLON;

        /// <summary>
        /// Default character to delimit values in an ADIF list.
        /// </summary>
        public const char VALUE_SEPARATOR = COMMA;

        /// <summary>
        /// Comma character.
        /// </summary>
        public const char COMMA = ',';

        /// <summary>
        /// Tab character.
        /// </summary>
        public const char TAB = '\t';

        /// <summary>
        /// Colon character.
        /// </summary>
        public const char COLON = ':';

        /// <summary>
        /// Opening curly brace character.
        /// </summary>
        public const char CURLY_BRACE_OPEN = '{';

        /// <summary>
        /// Closing curly brace character.
        /// </summary>
        public const char CURLY_BRACE_CLOSE = '}';

        /// <summary>
        /// Underscore character.
        /// </summary>
        public const char UNDERSCORE = '_';

        /// <summary>
        /// Default program ID used in generated ADIF files.
        /// </summary>
        public const string DEFAULT_PROGRAM_ID = "ADIFNET";

        /// <summary>
        /// Default header text on the first line of an ADIF file.
        /// </summary>
        public const string DEFAULT_ADIF_HEADER_TEXT = "ADIF generated by " + DEFAULT_PROGRAM_ID;

        /// <summary>
        /// String representing a true boolean value.
        /// </summary>
        public const string ADIF_BOOLEAN_TRUE = "Y";

        /// <summary>
        /// String representing a false boolean value.
        /// </summary>
        public const string ADIF_BOOLEAN_FALSE = "N";

        /// <summary>
        /// Display string for a true boolean value.
        /// </summary>
        public const string ADIF_BOOLEAN_TRUE_DISPLAY = "Yes";

        /// <summary>
        /// Display string for a false boolean value.
        /// </summary>
        public const string ADIF_BOOLEAN_FALSE_DISPLAY = "No";

        /// <summary>
        /// 
        /// </summary>
        public const string ADIF_NET_CONFIG_FILE_NAME = "adifnet.properties";

        /// <summary>
        /// Regex used to match the ADIF SOTARef data type.
        /// </summary>
        public const string SOTA_REF_REGEX = @"[a-zA-Z0-9]{1,8}\/[a-zA-Z]{2}\-([0-9][0-9][1-9]|[0-9][1-9][0-9]|[1-9][0-9][0-9])";

        /// <summary>
        /// Regex used to match callsigns.
        /// </summary>
        public const string CALLSIGN_REGEX = @"^((\d|[A-Z])+\/)?((\d|[A-Z]){3,})(\/(\d|[A-Z])+)?(\/(\d|[A-Z])+)?$";

        /// <summary>
        /// ADIF version.
        /// </summary>
        public static readonly Version ADIFVersion = new(3, 1, 0);

        /// <summary>
        /// The current version of ADIF.NET
        /// </summary>
        public static readonly Version ProgramVersion = Assembly.GetExecutingAssembly().GetName().Version ?? new(1, 0);

        /// <summary>
        /// QSO upload status enumeration.
        /// </summary>
        public static readonly AdifEnumeration QsoUploadStatuses;

        /// <summary>
        /// QSO completion status enumeration.
        /// </summary>
        public static readonly AdifEnumeration QsoCompleteStatuses;

        /// <summary>
        /// QSL via enumeration.
        /// </summary>
        public static readonly AdifEnumeration Via;

        /// <summary>
        /// Antenna path enumeration.
        /// </summary>
        public static readonly AdifEnumeration AntennaPaths;

        /// <summary>
        /// QSL medium enumeration.
        /// </summary>
        public static readonly AdifEnumeration QslMediums;

        /// <summary>
        /// Continent enumeration.
        /// </summary>
        public static readonly AdifEnumeration Continents;

        /// <summary>
        /// eSQL sent status enumeration.
        /// </summary>
        public static readonly AdifEnumeration EQslSentStatuses;

        /// <summary>
        /// eQSL received status enumeration.
        /// </summary>
        public static readonly AdifEnumeration EQslReceivedStatuses;

        /// <summary>
        /// Propagation mode enumeration.
        /// </summary>
        public static readonly AdifEnumeration PropagationModes;

        /// <summary>
        /// ARRL section enumeration.
        /// </summary>
        public static readonly AdifEnumeration ArrlSections;

        /// <summary>
        /// Award enumeration.
        /// </summary>
        public static readonly AdifEnumeration Awards;

        /// <summary>
        /// Mode enumeration.
        /// </summary>
        public static readonly AdifEnumeration Modes;

        /// <summary>
        /// Sub-mode enumeration.
        /// </summary>
        public static readonly AdifEnumeration SubModes;

        /// <summary>
        /// Sponsored award prefix enumeration.
        /// </summary>
        public static readonly AdifEnumeration SponsoredAwardPrefixes;

        /// <summary>
        /// Country code enumeration.
        /// </summary>
        public static readonly AdifEnumeration CountryCodes;

        /// <summary>
        /// Contest enumeration.
        /// </summary>
        public static readonly AdifEnumeration Contests;

        /// <summary>
        /// Amateur radio band enumeration
        /// </summary>
        public static readonly AdifEnumeration Bands;

        /// <summary>
        /// Credit enumeration.
        /// </summary>
        public static readonly AdifEnumeration Credits;

        /// <summary>
        /// QSL received statuses.
        /// </summary>
        public static readonly AdifEnumeration QslReceivedStatuses;

        /// <summary>
        /// QSL sent statuses.
        /// </summary>
        public static readonly AdifEnumeration QslSentStatuses;

        /// <summary>
        /// Boolean values.
        /// </summary>
        public static readonly AdifEnumeration BooleanValues;

        /// <summary>
        /// 
        /// </summary>
        public static readonly AdifEnumeration DarcDoks;

        /// <summary>
        /// Primary administrative subdivisions.
        /// </summary>
        public static readonly AdifEnumeration PrimarySubdivisions;

        /// <summary>
        /// Secondary administrative subdivisions.
        /// </summary>
        public static readonly AdifEnumeration SecondarySubdivisions;

        /// <summary>
        /// 
        /// </summary>
        public static readonly AdifEnumeration Regions;

        /// <summary>
        /// 
        /// </summary>
        public static readonly AdifEnumeration ArrlPrecedence;

        /// <summary>
        /// User configuration for ADIF.NET
        /// </summary>
        public static readonly Configuration Configuration;


        /// <summary>
        /// Instantiates the static data fields for the class.
        /// </summary>
        static Values()
        {
            QsoUploadStatuses = AdifEnumeration.Get("QSOUploadStatus") ?? [];
            QsoCompleteStatuses = AdifEnumeration.Get("QSOCompleteStatus") ?? [];
            Via = AdifEnumeration.Get("Via") ?? [];
            AntennaPaths = AdifEnumeration.Get("AntennaPath") ?? [];
            QslMediums = AdifEnumeration.Get("QSLMedium") ?? [];
            Continents = AdifEnumeration.Get("Continent") ?? [];
            EQslSentStatuses = AdifEnumeration.Get("EQSLSentStatus") ?? [];
            EQslReceivedStatuses = AdifEnumeration.Get("EQSLReceivedStatus") ?? [];
            PropagationModes = AdifEnumeration.Get("PropagationMode") ?? [];
            ArrlSections = AdifEnumeration.Get("ARRLSection") ?? [];
            Awards = AdifEnumeration.Get("Award") ?? [];
            Modes = AdifEnumeration.Get("Mode") ?? [];
            SubModes = AdifEnumeration.Get("Submode") ?? [];
            SponsoredAwardPrefixes = AdifEnumeration.Get("SponsoredAwardPrefix") ?? [];
            CountryCodes = AdifEnumeration.Get("DXCC") ?? [];
            Contests = AdifEnumeration.Get("ContestID") ?? [];
            Bands = AdifEnumeration.Get("Band") ?? [];
            Credits = AdifEnumeration.Get("Credit") ?? [];
            QslSentStatuses = AdifEnumeration.Get("QSLSent") ?? [];
            QslReceivedStatuses = AdifEnumeration.Get("QSLRcvd") ?? [];
            BooleanValues = AdifEnumeration.Get(nameof(AdifBoolean)) ?? [];
            DarcDoks = AdifEnumeration.Get("DARCDOK") ?? [];
            Regions = AdifEnumeration.Get("Region") ?? [];
            PrimarySubdivisions = AdifEnumeration.Get("PrimarySubdivision") ?? [];
            SecondarySubdivisions = AdifEnumeration.Get("SecondarySubdivision") ?? [];
            ArrlPrecedence = AdifEnumeration.Get("ARRLPrecedence") ?? [];
            Configuration = new Configuration();
        }
    }

    /// <summary>
    /// XML element names for parsing files in ADX format.
    /// </summary>
    public static class ADXValues
    {
        /// <summary>
        /// The root ADX element.
        /// </summary>
        public const string ADX_ROOT_ELEMENT = "ADX";

        /// <summary>
        /// The RECORDS element in ADX.
        /// </summary>
        public const string ADX_RECORDS_ELEMENT = "RECORDS";

        /// <summary>
        /// The RECORD element in ADX.
        /// </summary>
        public const string ADX_RECORD_ELEMENT = "RECORD";

        /// <summary>
        /// The HEADER element in ADX.
        /// </summary>
        public const string ADX_HEADER_ELEMENT = "HEADER";

        /// <summary>
        /// The ENUM attribute in ADX.
        /// </summary>
        public const string ADX_ENUM_ATTRIBUTE = "ENUM";

        /// <summary>
        /// The RANGE attribute in ADX.
        /// </summary>
        public const string ADX_RANGE_ATTRIBUTE = "RANGE";

        /// <summary>
        /// The FIELDID attribute in ADX.
        /// </summary>
        public const string ADX_FIELDID_ATTRIBUTE = "FIELDID";

        /// <summary>
        /// The TYPE attribute in ADX.
        /// </summary>
        public const string ADX_TYPE_ATTRIBUTE = "TYPE";

        /// <summary>
        /// The PROGRAMID attribute in ADX.
        /// </summary>
        public const string ADX_PROGRAMID_ATTRIBUTE = "PROGRAMID";

        /// <summary>
        /// The APP element in ADX.
        /// </summary>
        public const string ADX_APP_ELEMENT = "APP";

        /// <summary>
        /// The FIELDNAME attribute in ADX.
        /// </summary>
        public const string ADX_FIELDNAME_ATTRIBUTE = "FIELDNAME";
    }

    /// <summary>
    /// ADIF data type abbreviations.
    /// </summary>
    [Enumeration]
    public static class DataTypes
    {
        /// <summary>
        /// ADIF AwardList data type.
        /// </summary>
        public const string AwardList = "A";

        /// <summary>
        /// ADIF CreditList data type.
        /// </summary>
        public const string CreditList = "C";

        /// <summary>
        /// ADIF SponsoredAwardList data type.
        /// </summary>
        public const string SponsoredAwardList = "P";

        /// <summary>
        /// ADIF Boolean type.
        /// </summary>
        public const string Boolean = "B";

        /// <summary>
        /// ADIF Digit type.
        /// </summary>
        public const string Digit = "N";

        /// <summary>
        /// ADIF Number type.
        /// </summary>
        public const string Number = "N";

        /// <summary>
        /// ADIF Character type.
        /// </summary>
        public const string Character = "S";

        /// <summary>
        /// ADIF IntlCharacter type.
        /// </summary>
        public const string IntlCharacter = "I";

        /// <summary>
        /// ADIF Date type.
        /// </summary>
        public const string Date = "D";

        /// <summary>
        /// ADIF Time type.
        /// </summary>
        public const string Time = "T";

        /// <summary>
        /// ADIF String type.
        /// </summary>
        public const string String = "S";

        /// <summary>
        /// ADIF IntlString type.
        /// </summary>
        public const string IntlString = "I";

        /// <summary>
        /// ADIF MultilineString type.
        /// </summary>
        public const string MultilineString = "M";

        /// <summary>
        /// ADIF IntlMultilineString type.
        /// </summary>
        public const string IntlMultilineString = "G";

        /// <summary>
        /// ADIF Enumeration type.
        /// </summary>
        public const string Enumeration = "E";

        /// <summary>
        /// ADIF Location type.
        /// </summary>
        public const string Location = "L";
    }

    /// <summary>
    /// ADIF data type names.
    /// </summary>
    public class DataTypeNames
    {
        /// <summary>
        /// A comma-delimited list of members of the ADIF Award enumeration.
        /// </summary>
        public const string AwardList = "AwardList";

        /// <summary>
        /// A comma-delimited list where each list item is either: a member of the ADIF Credit enumeration or a member of the 
        /// ADIF Credit enumeration followed by a colon and an ampersand-delimited list of members of the ADIF QSL_Medium enumeration.
        /// </summary>
        public const string CreditList = "CreditList";

        /// <summary>
        /// A comma-delimited list of members of the ADIF Sponsored_Award enumeration.
        /// </summary>
        public const string SponsoredAwardList = "SponsoredAwardList";

        /// <summary>
        /// An ADIF boolean value where true is represented by 'Y' or 'y' and false is 
        /// represented by 'N' or 'n'.
        /// </summary>
        public const string Boolean = "Boolean";

        /// <summary>
        /// An ASCII character whose code lies in the range of 48 through 57, inclusive.
        /// </summary>
        public const string Digit = "Digit";

        /// <summary>
        /// A sequence of one or more Digits representing a decimal number, optionally preceded by a minus sign and optionally 
        /// including a single decimal point.
        /// </summary>
        public const string Number = "Number";

        /// <summary>
        /// An ASCII character whose code lies in the range of 32 through 126, inclusive.
        /// </summary>
        public const string Character = "Character";

        /// <summary>
        /// A Unicode character (encoded with UTF-8) excluding line break CR (code 13) and LF (code 10) characters.
        /// </summary>
        public const string IntlCharacter = "IntlCharacter";

        /// <summary>
        /// ADIF Date type consisting of 8 Digits representing a UTC date in YYYYMMDD format.
        /// </summary>
        public const string Date = "Date";

        /// <summary>
        /// ADIF Time type consisting of 6 Digits representing a UTC time in HHMMSS format or 4 Digits 
        /// representing a UTC time in HHMM format.
        /// </summary>
        public const string Time = "Time";

        /// <summary>
        /// ADIF String type consisting of a sequence of Characters.
        /// </summary>
        public const string String = "String";

        /// <summary>
        /// ADIF IntlString type consisting of a sequence of International Characters. 
        /// </summary>
        public const string IntlString = "IntlString";

        /// <summary>
        /// ADIF MultilineString type consisting of a sequence of Characters and line-breaks, where a line break is an ASCII CR (code 13) 
        /// followed immediately by an ASCII LF (code 10).
        /// </summary>
        public const string MultilineString = "MultilineString";

        /// <summary>
        /// ADIF IntlMultilineString type consisting of a sequence of International Characters and line breaks. 
        /// </summary>
        public const string IntlMultilineString = "IntlMultilineString";

        /// <summary>
        /// ADIF Enumeration type consisting of an explicit list of legal case-insensitive values represented in ASCII 
        /// in set notation, e.g. {A, B, C, D}, or defined in a table.
        /// </summary>
        public const string Enumeration = "Enumeration";

        /// <summary>
        /// ADIF Location type consisting of a sequence of 11 characters representing a latitude or longitude in XDDD MM.MMM format, 
        /// where X is a directional character from the set {E, W, N, S}, DDD is a 3-digit degrees specifier, where 
        /// 0 &lt;= DDD &lt;= 180, and MM.MMM is an unsigned Number minutes specifier with its decimal point 
        /// in the third position, where 00.000 &lt;= MM.MMM &lt;= 59.999. 
        /// </summary>
        public const string Location = "Location";

        /// <summary>
        /// ADIF GridSquare type consisting of a case-insensitive 2-character, 4-character, 6-character, or 8-character Maidenhead locator.
        /// </summary>
        public const string GridSquare = "GridSquare";

        /// <summary>
        /// ADIF GridSquareList type consisting of a comma-delimited list of <see cref="GridSquare"/> items.
        /// </summary>
        public const string GridSquareList = "GridSquareList";

        /// <summary>
        /// ADIF PositiveInteger type: an unsigned sequence of one or more <see cref="Digit"/>s representing a decimal 
        /// integer that has a value greater than 0.
        /// </summary>
        public const string PositiveInteger = "PositiveInteger";

        /// <summary>
        /// ADIF SecondarySubdivisionList type: a colon-delimited list of two or more members of the ADIF Secondary_Administrative_Subdivision enumeration.
        /// </summary>
        public const string SecondarySubdivisionList = "SecondarySubdivisionList";

        /// <summary>
        /// ADIF SOTARef type: a sequence of <see cref="Character"/>s representing an International SOTA Reference.
        /// </summary>
        public const string SOTARef = "SOTARef";

        /// <summary>
        /// ADIF IOTARefNo type: an IOTA designator, in format CC-XXX, where CC is a member of the ADIF Continent enumeration
        /// and XXX is the island group designator, where 1 &lt;= XXX &lt;= 999 with leading zeros.
        /// </summary>
        public const string IOTARefNo = "IOTARefNo";

        /// <summary>
        /// ADIF Integer type: a sequence of one or more <see cref="Digit"/>s representing a decimal integer, optionally preceded 
        /// by a minus sign (ASCII code 45).
        /// </summary>
        public const string Integer = "Integer";
    }

    /// <summary>
    /// Represents an amateur radio band.
    /// </summary>
    public class Band
    {
        /// <summary>
        /// The name of the band.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The upper frequency of the band.
        /// </summary>
        public double UpperFrequency { get; set; }

        /// <summary>
        /// The lower frequency of the band.
        /// </summary>
        public double LowerFrequency { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Band"/> class.
        /// </summary>
        /// <param name="value">Value from the database.</param>
        public Band(dynamic value)
        {
            if (value is IDictionary<string, object?> dict)
            {
                if (dict.TryGetValue(nameof(Name), out object? bandName) && bandName is string _bandName)
                    Name = _bandName;

                if (dict.TryGetValue(nameof(UpperFrequency), out object? upperFreq) && upperFreq is double _upperFreq)
                    UpperFrequency = _upperFreq;

                if (dict.TryGetValue(nameof(LowerFrequency), out object? lowerFreq) && lowerFreq is double _lowerFreq)
                    LowerFrequency = _lowerFreq;
            }
            Name ??= string.Empty;
        }

        /// <summary>
        /// Retrieves all amateur radio bands.
        /// </summary>
        public static List<Band> Get()
        {
            var bands = new List<Band>();

            var data = SQLiteHelper.Instance.ReadData(GET_BANDS_SQL);
            foreach (var d in data)
            {
                var band = new Band(d);
                if (band != null)
                    bands.Add(band);
            }

            return bands;
        }

        /// <summary>
        /// Determines whether or not the specified frequency is a valid amateur radio frequency.
        /// </summary>
        /// <param name="frequency">Frequency to validate.</param>
        public static bool IsAmateurFrequency(double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_SQL,
                                                      new Dictionary<string, object?>() { { "@Frequency", frequency } });
            return data.Count > 0;
        }

        /// <summary>
        /// Retrieves the amateur radio band associated with the specified frequency.
        /// </summary>
        /// <param name="frequency">Frequency value.</param>
        public static Band? Get(double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_SQL,
                                                      new Dictionary<string, object?>() { { "@Frequency", frequency } });
            if (data.Count > 0 && data[0] is ExpandoObject dataObj)
                return new Band(dataObj);

            return null;
        }

        /// <summary>
        /// Determines whether or not the specified frequency is in the specified band.
        /// </summary>
        /// <param name="band">Amateur radio band.</param>
        /// <param name="frequency">Frequency value.</param>
        /// <returns>True if the frequency is in the band, else false.</returns>
        public static bool IsFrequencyInBand(string band, double frequency)
        {
            var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_BAND_SQL,
                                                      new Dictionary<string, object?>() { { "@Frequency", frequency },
                                                                                          { "@Name", band } });
            return data.Count > 0;
        }

        const string GET_UPPER_FREQENCY_SQL = "SELECT UpperFrequency FROM \"Bands\" WHERE Name = @Name";
        const string GET_LOWER_FREQENCY_SQL = "SELECT LowerFrequency FROM \"Bands\" WHERE Name = @Name";
        const string GET_BANDS_SQL = "SELECT Name, LowerFrequency, UpperFrequency FROM \"Bands\"";
        const string VALIDATE_FREQUENCY_SQL = "SELECT Name, LowerFrequency, UpperFrequency FROM \"Bands\" WHERE @Frequency >= LowerFrequency AND @Frequency <= UpperFrequency";
        const string VALIDATE_FREQUENCY_BAND_SQL = "SELECT Name, LowerFrequency, UpperFrequency FROM \"Bands\" WHERE @Frequency >= LowerFrequency AND @Frequency <= UpperFrequency AND Name = @Name";
    }
}

