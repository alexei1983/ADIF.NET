using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using ADIF.NET.Attributes;
using ADIF.NET.Helpers;
using ADIF.NET.Tags;

namespace ADIF.NET {

  public static class Values {

    public static byte ITU { get; set; }

    public const string ADIF_DATE_FORMAT = "yyyyMMdd";
    public const string ADIF_TIME_FORMAT_LONG = "HHmmss";
    public const string ADIF_TIME_FORMAT_SHORT = "HHmm";
    public const string EMAIL_ADDRESS_REGEX = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
    public const char TAG_OPENING = '<';
    public const char TAG_CLOSING = '>';
    public const char COMMENT_INDICATOR = '#';
    public const char LINE_ENDING = '\n';
    public const char AMPERSAND = '&';
    public const char LIST_DELIMITER = COLON;
    public const char VALUE_LENGTH_CHAR = COLON;
    public const char VALUE_SEPARATOR = COMMA;
    public const char COMMA = ',';
    public const char TAB = '\t';
    public const char COLON = ':';
    public const char CURLY_BRACE_OPEN = '{';
    public const char CURLY_BRACE_CLOSE = '}';
    public const char UNDERSCORE = '_';
    public const string DEFAULT_PROGRAM_ID = "ADIF.NET";
    public static readonly Version ADIFVersion = new Version(3, 1, 0);

    /// <summary>
    /// QSO upload status enumeration.
    /// </summary>
    public static readonly ADIFEnumeration QSOUploadStatuses;

    /// <summary>
    /// QSO completion status enumeration.
    /// </summary>
    public static readonly ADIFEnumeration QSOCompleteStatuses;

    /// <summary>
    /// QSL via enumeration.
    /// </summary>
    public static readonly ADIFEnumeration Via;

    /// <summary>
    /// Antenna path enumeration.
    /// </summary>
    public static readonly ADIFEnumeration AntennaPaths;

    /// <summary>
    /// QSL medium enumeration.
    /// </summary>
    public static readonly ADIFEnumeration QSLMediums;

    /// <summary>
    /// Continent enumeration.
    /// </summary>
    public static readonly ADIFEnumeration Continents;

    /// <summary>
    /// eSQL sent status enumeration.
    /// </summary>
    public static readonly ADIFEnumeration EQSLSentStatuses;

    /// <summary>
    /// eQSL received status enumeration.
    /// </summary>
    public static readonly ADIFEnumeration EQSLReceivedStatuses;

    /// <summary>
    /// Propagation mode enumeration.
    /// </summary>
    public static readonly ADIFEnumeration PropagationModes;

    /// <summary>
    /// ARRL section enumeration.
    /// </summary>
    public static readonly ADIFEnumeration ARRLSections;

    /// <summary>
    /// Award enumeration.
    /// </summary>
    public static readonly ADIFEnumeration Awards;

    /// <summary>
    /// Mode enumeration.
    /// </summary>
    public static readonly ADIFEnumeration Modes;

    /// <summary>
    /// Sponsored award prefix enumeration.
    /// </summary>
    public static readonly ADIFEnumeration SponsoredAwardPrefixes;

    /// <summary>
    /// Country code enumeration.
    /// </summary>
    public static readonly ADIFEnumeration CountryCodes;

    /// <summary>
    /// Contest enumeration.
    /// </summary>
    public static readonly ADIFEnumeration Contests;

    /// <summary>
    /// Amateur radio band enumeration
    /// </summary>
    public static readonly ADIFEnumeration Bands;

    /// <summary>
    /// Instantiates the static data fields for the class.
    /// </summary>
    static Values()
    {
      QSOUploadStatuses = ADIFEnumeration.Get("QSOUploadStatus");
      QSOCompleteStatuses = ADIFEnumeration.Get("QSOCompleteStatus");
      Via = ADIFEnumeration.Get("Via");
      AntennaPaths = ADIFEnumeration.Get("AntennaPath");
      QSLMediums = ADIFEnumeration.Get("QSLMedium");
      Continents = ADIFEnumeration.Get("Continent");
      EQSLSentStatuses = ADIFEnumeration.Get("EQSLSentStatus");
      EQSLReceivedStatuses = ADIFEnumeration.Get("EQSLReceivedStatus");
      PropagationModes = ADIFEnumeration.Get("PropagationMode");
      ARRLSections = ADIFEnumeration.Get("ARRLSection");
      Awards = ADIFEnumeration.Get("Award");
      Modes = ADIFEnumeration.Get("Mode");
      SponsoredAwardPrefixes = ADIFEnumeration.Get("SponsoredAwardPrefix");
      CountryCodes = ADIFEnumeration.Get("Countries");
      Contests = ADIFEnumeration.Get("Contest");
      Bands = ADIFEnumeration.Get("Band");
    }
  }

  /// <summary>
  /// Defines the names of ADIF tags.
  /// </summary>
  public static class TagNames {

    public const string Address = "ADDRESS";
    public const string AddressIntl = "ADDRESS_INTL";
    public const string ADIFVer = "ADIF_VER";
    public const string Age = "AGE";
    public const string AntAz = "ANT_AZ";
    public const string AntEl = "ANT_EL";
    public const string ARRLSect = "ARRL_SECT";
    public const string AntPath = "ANT_PATH";
    public const string AIndex = "A_INDEX";
    public const string AppDef = "APP_";
    public const string Band = "BAND";
    public const string BandRx = "BAND_RX";
    public const string Call = "CALL";
    public const string Check = "CHECK";
    public const string Class = "CLASS";
    public const string ClubLogQSOUploadDate = "CLUBLOG_QSO_UPLOAD_DATE";
    public const string ClubLogQSOUploadStatus = "CLUBLOG_QSO_UPLOAD_STATUS";
    public const string Cnty = "CNTY";
    public const string Comment = "COMMENT";
    public const string CommentIntl = "COMMENT_INTL";
    public const string Continent = "CONT";
    public const string ContactedOp = "CONTACTED_OP";
    public const string ContestId = "CONTEST_ID";
    public const string Country = "COUNTRY";
    public const string CountryIntl = "COUNTRY_INTL";
    public const string Cqz = "CQZ";
    public const string CreatedTimestamp = "CREATED_TIMESTAMP";
    public const string CreditSubmitted = "CREDIT_SUBMITTED";
    public const string CreditGranted = "CREDIT_GRANTED";
    public const string Distance = "DISTANCE";
    public const string Dxcc = "DXCC";
    public const string Email = "EMAIL";
    public const string EqCall = "EQ_CALL";
    public const string EndHeader = "EOH";
    public const string EndRecord = "EOR";
    public const string EQSLReceivedDate = "EQSL_QSLRDATE";
    public const string EQSLSentDate = "EQSL_QSLSDATE";
    public const string EQSLReceivedStatus = "EQSL_QSL_RCVD";
    public const string EQSLSentStatus = "EQSL_QSL_SENT";
    public const string Fists = "FISTS";
    public const string FistsCc = "FISTS_CC";
    public const string ForceInit = "FORCE_INIT";
    public const string Freq = "FREQ";
    public const string FreqRx = "FREQ_RX";
    public const string GridSquare = "GRIDSQUARE";
    public const string GuestOp = "GUEST_OP";
    public const string HrdLogQSOUploadDate = "HRDLOG_QSO_UPLOAD_DATE";
    public const string HrdLogQSOUploadStatus = "HRDLOG_QSO_UPLOAD_STATUS";
    public const string Iota = "IOTA";
    public const string IotaIslandId = "IOTA_ISLAND_ID";
    public const string Ituz = "ITUZ";
    public const string KIndex = "K_INDEX";
    public const string Lat = "LAT";
    public const string Lon = "LON";
    public const string LotwQSLReceivedDate = "LOTW_QSLRDATE";
    public const string LotwQSLSentDate = "LOTW_QSLSDATE";
    public const string LotwQSLReceivedStatus = "LOTW_QSL_RCVD";
    public const string LotwQSLSentStatus = "LOTW_QSL_SENT";
    public const string MaxBursts = "MAX_BURSTS";
    public const string Mode = "MODE";
    public const string MsShower = "MS_SHOWER";
    public const string MyAntenna = "MY_ANTENNA";
    public const string MyAntennaIntl = "MY_ANTENNA_INTL";
    public const string MyCity = "MY_CITY";
    public const string MyCityIntl = "MY_CITY_INTL";
    public const string MyCnty = "MY_CNTY";
    public const string MyCountry = "MY_COUNTRY";
    public const string MyCountryIntl = "MY_COUNTRY_INTL";
    public const string MyCqZone = "MY_CQ_ZONE";
    public const string MyDxcc = "MY_DXCC";
    public const string MyFists = "MY_FISTS";
    public const string MyGridSquare = "MY_GRIDSQUARE";
    public const string MyIota = "MY_IOTA";
    public const string MyIotaIslandId = "MY_IOTA_ISLAND_ID";
    public const string MyITUZone = "MY_ITU_ZONE";
    public const string MyLat = "MY_LAT";
    public const string MyLon = "MY_LON";
    public const string MyName = "MY_NAME";
    public const string MyNameIntl = "MY_NAME_INTL";
    public const string MyPostalCode = "MY_POSTAL_CODE";
    public const string MyPostalCodeIntl = "MY_POSTAL_CODE_INTL";
    public const string MyRig = "MY_RIG";
    public const string MyRigIntl = "MY_RIG_INTL";
    public const string MySig = "MY_SIG";
    public const string MySigIntl = "MY_SIG_INTL";
    public const string MySigInfo = "MY_SIG_INFO";
    public const string MySigInfoIntl = "MY_SIG_INFO_INTL";
    public const string MySotaRef = "MY_SOTA_REF";
    public const string MyState = "MY_STATE";
    public const string MyStreet = "MY_STREET";
    public const string MyStreetIntl = "MY_STREET_INTL";
    public const string MyUsacaCounties = "MY_USACA_COUNTIES";
    public const string MyVuccGrids = "MY_VUCC_GRIDS";
    public const string Name = "NAME";
    public const string NameIntl = "NAME_INTL";
    public const string Notes = "NOTES";
    public const string NotesIntl = "NOTES_INTL";
    public const string NrBursts = "NR_BURSTS";
    public const string NrPings = "NR_PINGS";
    public const string OwnerCallSign = "OWNER_CALLSIGN";
    public const string Operator = "OPERATOR";
    public const string Pfx = "PFX";
    public const string Precedence = "PRECEDENCE";
    public const string ProgramId = "PROGRAMID";
    public const string ProgramVersion = "PROGRAMVERSION";
    public const string PropMode = "PROP_MODE";
    public const string PublicKey = "PUBLIC_KEY";
    public const string QrzQSOUploadDate = "QRZCOM_QSO_UPLOAD_DATE";
    public const string QSODateOff = "QSO_DATE_OFF";
    public const string QSODate = "QSO_DATE";
    public const string QSOReceivedDate = "QSLRDATE";
    public const string QSOSentDate = "QSLSDATE";
    public const string QSORandom = "QSO_RANDOM";
    public const string QTH = "QTH";
    public const string QTHIntl = "QTH_INTL";
    public const string Rig = "RIG";
    public const string RigIntl = "RIG_INTL";
    public const string RxPwr = "RX_PWR";
    public const string SatMode = "SAT_MODE";
    public const string SatName = "SAT_NAME";
    public const string Sfi = "SFI";
    public const string Sig = "SIG";
    public const string SigIntl = "SIG_INTL";
    public const string SigInfo = "SIG_INFO";
    public const string SigInfoIntl = "SIG_INFO_INTL";
    public const string SilentKey = "SILENT_KEY";
    public const string SKCC = "SKCC";
    public const string SotaRef = "SOTA_REF";
    public const string Srx = "SRX";
    public const string SrxString = "SRX_STRING";
    public const string Stx = "STX";
    public const string StxString = "STX_STRING";
    public const string SWL = "SWL";
    public const string TenTen = "TEN_TEN";
    public const string TimeOff = "TIME_OFF";
    public const string TimeOn = "TIME_ON";
    public const string TxPwr = "TX_PWR";
    public const string UkSmg = "UKSMG";
    public const string USACA_Counties = "USACA_COUNTIES";
    public const string UserDef = "USERDEF";
    public const string Vucc_Grids = "VUCC_GRIDS";
    public const string Web = "WEB";
    }

  /// <summary>
  /// Represents an ADIF enumeration.
  /// </summary>
  public class ADIFEnumeration : List<ADIFEnumerationValue> {

    /// <summary>
    /// The enumeration type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumeration"/> class.
    /// </summary>
    public ADIFEnumeration() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumeration"/> class.
    /// </summary>
    /// <param name="type">The enumeration type.</param>
    public ADIFEnumeration(string type) : this(type, null) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumeration"/> class.
    /// </summary>
    /// <param name="type">The enumeration type.</param>
    /// <param name="values">Values to add to the current enumeration.</param>
    public ADIFEnumeration(string type, params ADIFEnumerationValue[] values)
    {
      this.Type = type;
      if (values != null)
      {
        foreach (var v in values)
        {
          if (v != null)
            this.Add(v);
        }
      }
    }

    /// <summary>
    /// Creates an <see cref="ADIFEnumeration"/> object using the custom options in a user-defined tag.
    /// </summary>
    /// <param name="tag">User-defined tag from which to derive the enumeration.</param>
    public static ADIFEnumeration FromUserDefinedTag(UserDefTag tag)
    {
      if (tag == null)
        throw new ArgumentNullException(nameof(tag), "User-defined tag is required.");

      if (tag.CustomOptions != null)
      {
        var enumVal = new ADIFEnumeration(tag.FieldName);
        foreach (var option in tag.CustomOptions)
          enumVal.Add(new ADIFEnumerationValue(option));

        return enumVal;
      }

      return null;
    }

    /// <summary>
    /// Retrieves an ADIF enumeration by type.
    /// </summary>
    /// <param name="type">The enumeration type to retrieve.</param>
    public static ADIFEnumeration Get(string type)
    {
      if (string.IsNullOrEmpty(type))
        throw new Exception("Enumeration type is required.");

      var enumeration = new ADIFEnumeration(type);

      ArrayList data = null;

      type = type.ToUpper();

      if (type == "COUNTRIES")
        data = SQLiteHelper.Instance.ReadData(RETRIEVE_COUNTRY_CODES_SQL);
      else if (type == "BAND")
      {
        if (Values.ITU <= 0)
          Values.ITU = 2;

        data = SQLiteHelper.Instance.ReadData(RETRIEVE_BANDS_SQL.Replace("{{ITU}}", Values.ITU.ToString()));
      }
      else if (type == "CONTEST")
        data = SQLiteHelper.Instance.ReadData(RETRIEVE_CONTESTS_SQL);
      else
        data = SQLiteHelper.Instance.ReadData(ENUM_RETRIEVE_SQL.Replace("{{TYPE}}", type.Replace("'", "''")));

      foreach (dynamic d in data)
      {
        var enumVal = new ADIFEnumerationValue(d);
        if (!string.IsNullOrEmpty(enumVal.Code))
          enumeration.Add(enumVal);     
      }

      return enumeration.Count > 0 ? enumeration : null;
    }

    /// <summary>
    /// Determines whether or not the specified value is valid for the current <see cref="ADIFEnumeration"/>.
    /// </summary>
    /// <param name="value">Value to check for validity.</param>
    public bool IsValid(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof(value), "Value is required.");

      return GetValues().Contains(value);
    }

    /// <summary>
    /// Retrieves a string array of codes for the current <see cref="ADIFEnumeration"/>.
    /// </summary>
    public string[] GetValues()
    {
      return this.Select(v => v.Code).ToArray();
    }
  
    const string ENUM_RETRIEVE_SQL = "SELECT Code, DisplayName, ImportOnly, Legacy, Parent FROM \"Enumerations\" WHERE Type = '{{TYPE}}' ORDER BY DisplayName, Code";
    const string RETRIEVE_COUNTRY_CODES_SQL = "SELECT Code, Name AS DisplayName, Deleted AS ImportOnly, Deleted AS Legacy FROM \"CountryCodes\" ORDER BY Name, Code";
    const string RETRIEVE_BANDS_SQL = "SELECT Name AS Code, Name AS DisplayName, 0 AS Legacy, 0 AS ImportOnly FROM \"Bands\" WHERE ITU = {{ITU}}";
    const string RETRIEVE_CONTESTS_SQL = "SELECT Code, Name AS DisplayName, Deprecated AS Legacy, Deprecated AS ImportOnly FROM \"Contests\" ORDER BY Name, Code";

  }

  /// <summary>
  /// Represents a value in an ADIF enumeration.
  /// </summary>
  public class ADIFEnumerationValue : IFormattable {

    /// <summary>
    /// The code for the enumeration value.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// The display name of the enumeration value.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Whether or not the enumeration value is only valid on import.
    /// </summary>
    public bool ImportOnly { get; set; }

    /// <summary>
    /// Whether or not the enumeration value is a legacy value.
    /// </summary>
    public bool Legacy { get; set; }

    /// <summary>
    /// The parent code for the enumeration value.
    /// </summary>
    public string Parent { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumerationValue"/> class.
    /// </summary>
    /// <param name="code">The code for the enumeration value.</param>
    public ADIFEnumerationValue(string code) : this(code, null) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumerationValue"/> class.
    /// </summary>
    /// <param name="code">The code for the enumeration value.</param>
    /// <param name="displayName">The display name of the enumeration value.</param>
    public ADIFEnumerationValue(string code, string displayName) : this(code, displayName, false, false, null) { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumerationValue"/> class.
    /// </summary>
    /// <param name="code">The code for the enumeration value.</param>
    /// <param name="displayName">The display name of the enumeration value.</param>
    /// <param name="importOnly">Whether or not the enumeration value is only valid on import.</param>
    /// <param name="legacy">Whether or not the enumeration value is a legacy value.</param>
    /// <param name="parent">The parent code for the enumeration value.</param>
    public ADIFEnumerationValue(string code, string displayName, bool importOnly, bool legacy, string parent = null)
    {
      this.DisplayName = displayName;
      this.Code = Code;
      this.ImportOnly = importOnly;
      this.Legacy = legacy;
      this.Parent = parent;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFEnumerationValue"/> class.
    /// </summary>
    /// <param name="value">Dynamic object representing the enumeration value.</param>
    public ADIFEnumerationValue(dynamic value)
    {
      if (value is IDictionary<string, object> dict)
      {
        if (dict.ContainsKey(nameof(DisplayName)) && dict[nameof(DisplayName)] is string name)
          this.DisplayName = name;

        if (dict.ContainsKey(nameof(Code))) {
          if (dict[nameof(Code)] is string code)
            this.Code = code;
          else if (dict[nameof(Code)] is int intCode)
            this.Code = intCode.ToString();
          else if (dict[nameof(Code)] is double dblCode)
            this.Code = dblCode.ToString();
        }

        if (dict.ContainsKey(nameof(ImportOnly)) && dict[nameof(ImportOnly)] is bool importOnly)
          this.ImportOnly = importOnly;

        if (dict.ContainsKey(nameof(Legacy)) && dict[nameof(Legacy)] is bool legacy)
          this.Legacy = legacy;

        if (dict.ContainsKey(nameof(Parent)) && dict[nameof(Parent)] is string parent)
          this.Parent = parent;
      }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public override string ToString()
    {
      return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    public string ToString(string format)
    {
      return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    public string ToString(string format, IFormatProvider provider)
    {
      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format)
      {
        case "G":
        case "g":
          return ToString("E", provider);

        case "C":
          return Code ?? string.Empty;

        case "N":
          return DisplayName ?? string.Empty;

        case "I":
          return ImportOnly.ToString();

        case "L":
          return Legacy.ToString();

        case "E":
          return !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(DisplayName) ?
            $"{ToString("C", provider)} - {ToString("N", provider)}" :
            !string.IsNullOrEmpty(DisplayName) ? ToString("N", provider) :
            !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
            string.Empty;

        case "e":
          return !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(DisplayName) ?
            $"{ToString("N", provider)} - {ToString("C", provider)}" :
            !string.IsNullOrEmpty(DisplayName) ? ToString("N", provider) :
            !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
            string.Empty;

        default:
          throw new FormatException($"Format string '{format}' is not valid.");
      }
    }

  }

  /// <summary>
  /// Represents a country code.
  /// </summary>
  public class CountryCode {

    public int Code { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }

    public CountryCode(int code, string name, bool deleted) {
      this.Code = code;
      this.Name = name;
      this.Deleted = deleted;
    }

    public CountryCode(dynamic value)
    {
      if (value is IDictionary<string, object> dict)
      {
        if (dict.ContainsKey(nameof(Name)) && dict[nameof(Name)] is string name)
          this.Name = name;

        if (dict.ContainsKey(nameof(Code)) && dict[nameof(Code)] is int code)
          this.Code = code;

        if (dict.ContainsKey(nameof(Deleted)) && dict[nameof(Deleted)] is bool deleted)
          this.Deleted = deleted;
      }
    }

    public static List<CountryCode> Get()
    {
      var list = new List<CountryCode>();

      var data = SQLiteHelper.Instance.ReadData(RETRIEVE_COUNTRY_CODES_SQL);

      foreach (dynamic d in data)
      {
        var cc = new CountryCode(d);
        if (cc.Code > 0)
          list.Add(cc);
      }

      return list;
    }

    const string RETRIEVE_COUNTRY_CODES_SQL = "SELECT Code, Name, Deleted FROM \"CountryCodes\" ORDER BY Code";

  }

  /// <summary>
  /// Represents a contest.
  /// </summary>
  public class Contest  {
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Deprecated { get; set; }
    public DateTime? ValidStart { get; set; }
    public DateTime? ValidEnd { get; set; }

    public Contest(string code, string name, bool deprecated, DateTime? validStart, DateTime? validEnd)
    {
      this.Code = code;
      this.Name = name;
      this.Deprecated = deprecated;
      this.ValidEnd = validEnd;
      this.ValidStart = validStart;
    }

    public Contest(dynamic value)
    {
      if (value is IDictionary<string, object> dict)
      {
        if (dict.ContainsKey(nameof(Name)) && dict[nameof(Name)] is string name)
          this.Name = name;

        if (dict.ContainsKey(nameof(Code)) && dict[nameof(Code)] is string code)
          this.Code = code;

        if (dict.ContainsKey(nameof(Deprecated)) && dict[nameof(Deprecated)] is bool deprecated)
          this.Deprecated = deprecated;

        if (dict.ContainsKey(nameof(ValidStart)) && dict[nameof(ValidStart)] is long validStart)
        {
          if (validStart > 0)
            this.ValidStart = DateTimeOffset.FromUnixTimeSeconds(validStart).DateTime;
        }

        if (dict.ContainsKey(nameof(ValidEnd)) && dict[nameof(ValidEnd)] is long validEnd)
        {
          if (validEnd > 0)
            this.ValidEnd = DateTimeOffset.FromUnixTimeSeconds(validEnd).DateTime;
        }
      }
    }

    public static List<Contest> Get()
    {
      var list = new List<Contest>();
      var data = SQLiteHelper.Instance.ReadData(RETRIEVE_CONTESTS_SQL);

      foreach (dynamic d in data)
      {
        var contest = new Contest(d);
        if (!string.IsNullOrEmpty(contest.Code))
          list.Add(contest);
      }

      return list;
    }

    const string RETRIEVE_CONTESTS_SQL = "SELECT Code, Name, Deprecated, ValidStart, ValidEnd FROM \"Contests\" ORDER BY Code";
  }

  [Enumeration]
  public static class DataTypes {

    [DisplayName("Award List")]
    [ImportOnly]
    public const string AwardList = "A";

    [DisplayName("Credit List")]
    public const string CreditList = "C";

    [DisplayName("Sponsored Award List")]
    public const string SponsoredAwardList = "P";

    public const string Boolean = "B";

    public const string Digit = "N";

    public const string Number = "N";

    public const string Character = "S";

    [DisplayName("International Character")]
    public const string IntlCharacter = "I";

    public const string Date = "D";

    public const string Time = "T";

    public const string String = "S";

    [DisplayName("International String")]
    public const string IntlString = "I";

    [DisplayName("Multiline String")]
    public const string MultilineString = "M";

    [DisplayName("International Multiline String")]
    public const string IntlMultilineString = "G";

    public const string Enumeration = "E";

    public const string Location = "L";
  }

  /// <summary>
  /// Represents an amateur radio band.
  /// </summary>
  public class Band {

    public string Name { get; set; }
    public double UpperFrequency { get; set; }
    public double LowerFrequency { get; set; }
    public int ITU { get; set; }

    public Band(dynamic value)
    {
      if (value is IDictionary<string, object> dict)
      {
        if (dict.ContainsKey(nameof(Name)) && dict[nameof(Name)] is string name)
          this.Name = name;

        if (dict.ContainsKey(nameof(UpperFrequency)) && dict[nameof(UpperFrequency)] is double upperFrequency)
          this.UpperFrequency = upperFrequency;

        if (dict.ContainsKey(nameof(LowerFrequency)) && dict[nameof(LowerFrequency)] is double lowerFrequency)
          this.LowerFrequency = lowerFrequency;

        if (dict.ContainsKey(nameof(ITU)) && dict[nameof(ITU)] is long itu)
          this.ITU = itu < int.MaxValue ? (int)itu : 0;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itu"></param>
    /// <returns></returns>
    public static List<Band> Get(int itu)
    {
      if (itu < 1 || itu > 3)
        throw new ArgumentException("Invalid ITU region.");

      var bands = new List<Band>();

      var data = SQLiteHelper.Instance.ReadData(GET_BANDS_SQL.Replace("{{ITU}}", itu.ToString()));
      foreach (var d in data)
      {
        var band = new Band(d);
        if (band != null)
          bands.Add(band);
      }

      return bands;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="itu"></param>
    /// <returns></returns>
    public static bool IsAmateurFrequency(double frequency, int itu)
    {
      if (itu < 1 || itu > 3)
        throw new ArgumentException("Invalid ITU region.");

      var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_SQL.Replace("{{ITU}}", itu.ToString()).Replace("{{{FREQUENCY}}", frequency.ToString()));
      return data.Count > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="itu"></param>
    /// <returns></returns>
    public static Band Get(double frequency, int itu)
    {
      if (itu < 1 || itu > 3)
        throw new ArgumentException("Invalid ITU region.");

      var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_SQL.Replace("{{ITU}}", itu.ToString())
                                                    .Replace("{{FREQUENCY}}", frequency.ToString()));
      if (data.Count > 0)
        return new Band(data[0]);

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="band"></param>
    /// <param name="frequency"></param>
    /// <param name="itu"></param>
    /// <returns></returns>
    public static bool IsFrequencyInBand(string band, double frequency, int itu)
    {
      if (itu < 1 || itu > 3)
        throw new ArgumentException("Invalid ITU region.");

      var data = SQLiteHelper.Instance.ReadData(VALIDATE_FREQUENCY_BAND_SQL.Replace("{{ITU}}", itu.ToString())
                                                          .Replace("{{FREQUENCY}}", frequency.ToString())
                                                          .Replace("{{NAME}}", band));
      return data.Count > 0;
    }

    const string GET_UPPER_FREQENCY_SQL = "SELECT UpperFrequency FROM \"Bands\" WHERE Name = '{{NAME}}' AND ITU = {{ITU}}";
    const string GET_LOWER_FREQENCY_SQL = "SELECT LowerFrequency FROM \"Bands\" WHERE Name = '{{NAME}}' AND ITU = {{ITU}}";
    const string GET_BANDS_SQL = "SELECT Name, LowerFrequency, UpperFrequency, ITU FROM \"Bands\" WHERE ITU = {{ITU}}";
    const string VALIDATE_FREQUENCY_SQL = "SELECT Name, LowerFrequency, UpperFrequency, ITU FROM \"Bands\" WHERE ITU = {{ITU}} AND {{FREQUENCY}} >= LowerFrequency AND {{FREQUENCY}} <= UpperFrequency";
    const string VALIDATE_FREQUENCY_BAND_SQL = "SELECT Name, LowerFrequency, UpperFrequency, ITU FROM \"Bands\" WHERE ITU = {{ITU}} AND {{FREQUENCY}} >= LowerFrequency AND {{FREQUENCY}} <= UpperFrequency AND Name = '{{NAME}}'";

  }
}
