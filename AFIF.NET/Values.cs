using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using ADIF.NET.Attributes;
using ADIF.NET.Helpers;

namespace ADIF.NET {

  public static class Values {
    public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public const string AdifDateFormat = "yyyyMMdd";
    public const string AdifTimeFormatLong = "HHmmss";
    public const string AdifTimeFormatShort = "HHmm";
    public const string EmailAddressRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
    public const char TagOpening = '<';
    public const char TagClosing = '>';
    public const char CommentIndicator = '#';
    public const char LineEnding = '\n';
    public const char Ampersand = '&';
    public const char ListDelimiter = Colon;
    public const char ValueLengthChar = Colon;
    public const char DefaultValueSeparator = Comma;
    public const char Comma = ',';
    public const char Tab = '\t';
    public const char Colon = ':';
    public const char CurlyBraceOpen = '{';
    public const char CurlyBraceClose = '}';
    public const char Underscore = '_';
    public const string DefaultProgramId = "ADIF.NET";
    public static readonly Version AdifVersion = new Version(3, 1, 0);
    }

  /// <summary>
  /// Defines the names of ADIF tags.
  /// </summary>
  public static class TagNames {

    public const string Address = "ADDRESS";
    public const string AddressIntl = "ADDRESS_INTL";
    public const string AdifVer = "ADIF_VER";
    public const string Age = "AGE";
    public const string AntAz = "ANT_AZ";
    public const string AntEl = "ANT_EL";
    public const string ArrlSect = "ARRL_SECT";
    public const string AntPath = "ANT_PATH";
    public const string AIndex = "A_INDEX";
    public const string AppDef = "APP_";
    public const string Band = "BAND";
    public const string BandRx = "BAND_RX";
    public const string Call = "CALL";
    public const string Check = "CHECK";
    public const string Class = "CLASS";
    public const string ClubLogQsoUploadDate = "CLUBLOG_QSO_UPLOAD_DATE";
    public const string ClubLogQsoUploadStatus = "CLUBLOG_QSO_UPLOAD_STATUS";
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
    public const string EQslReceivedDate = "EQSL_QSLRDATE";
    public const string EQslSentDate = "EQSL_QSLSDATE";
    public const string EQslReceivedStatus = "EQSL_QSL_RCVD";
    public const string EQslSentStatus = "EQSL_QSL_SENT";
    public const string Fists = "FISTS";
    public const string FistsCc = "FISTS_CC";
    public const string ForceInit = "FORCE_INIT";
    public const string Freq = "FREQ";
    public const string FreqRx = "FREQ_RX";
    public const string GridSquare = "GRIDSQUARE";
    public const string GuestOp = "GUEST_OP";
    public const string HrdLogQsoUploadDate = "HRDLOG_QSO_UPLOAD_DATE";
    public const string HrdLogQsoUploadStatus = "HRDLOG_QSO_UPLOAD_STATUS";
    public const string Iota = "IOTA";
    public const string IotaIslandId = "IOTA_ISLAND_ID";
    public const string Ituz = "ITUZ";
    public const string KIndex = "K_INDEX";
    public const string Lat = "LAT";
    public const string Lon = "LON";
    public const string LotwQslReceivedDate = "LOTW_QSLRDATE";
    public const string LotwQslSentDate = "LOTW_QSLSDATE";
    public const string LotwQslReceivedStatus = "LOTW_QSL_RCVD";
    public const string LotwQslSentStatus = "LOTW_QSL_SENT";
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
    public const string MyItuZone = "MY_ITU_ZONE";
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
    public const string QrzQsoUploadDate = "QRZCOM_QSO_UPLOAD_DATE";
    public const string QsoDateOff = "QSO_DATE_OFF";
    public const string QsoDate = "QSO_DATE";
    public const string QsoReceivedDate = "QSLRDATE";
    public const string QsoSentDate = "QSLSDATE";
    public const string QsoRandom = "QSO_RANDOM";
    public const string Qth = "QTH";
    public const string QthIntl = "QTH_INTL";
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
    public const string Skcc = "SKCC";
    public const string SotaRef = "SOTA_REF";
    public const string Srx = "SRX";
    public const string SrxString = "SRX_STRING";
    public const string Stx = "STX";
    public const string StxString = "STX_STRING";
    public const string Swl = "SWL";
    public const string TenTen = "TEN_TEN";
    public const string TimeOff = "TIME_OFF";
    public const string TimeOn = "TIME_ON";
    public const string TxPwr = "TX_PWR";
    public const string UkSmg = "UKSMG";
    public const string Usaca_Counties = "USACA_COUNTIES";
    public const string UserDef = "USERDEF";
    public const string Vucc_Grids = "VUCC_GRIDS";
    public const string Web = "WEB";
    }

  /// <summary>
  /// 
  /// </summary>
  public class Option : IFormattable {

    public string Code { get; set; }

    public string Name { get; set; }

    public string[] AlternateNames { get; set; }

    public bool Legacy { get; set; }

    public bool Deleted { get; set; }

    public override string ToString() {
      return ToString("G", CultureInfo.CurrentCulture);
      }

    public string ToString(string format) {
      return ToString(format, CultureInfo.CurrentCulture);
      }

    public string ToString(string format, IFormatProvider provider) {

      if (string.IsNullOrEmpty(format))
        format = "G";

      if (provider == null)
        provider = CultureInfo.CurrentCulture;

      switch (format) {

      case "G":
        return ToString("O", provider);

      case "C":
        return Code ?? string.Empty;

      case "N":
        return Name ?? string.Empty;

      case "L":
        return Legacy.ToAdifBooleanValue();

      case "D":
        return Deleted.ToAdifBooleanValue();

      case "O":
        return !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Name) ? 
          $"{ToString("C", provider)} - {ToString("N", provider)}" : 
          !string.IsNullOrEmpty(Name) ? ToString("N", provider) : 
          !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
          string.Empty;

      case "o":
        return !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Name) ?
          $"{ToString("N", provider)} ({ToString("C", provider)})" :
          !string.IsNullOrEmpty(Name) ? ToString("N", provider) :
          !string.IsNullOrEmpty(Code) ? ToString("C", provider) :
          string.Empty;

      default:
        throw new FormatException($"Format string '{format}' is not valid.");
        }
      }
    }

  [Enumeration]
  public class ArrlSection {

    [DisplayName("Alabama")]
    public const string AL = "AL";
    [DisplayName("Alaska")]
    public const string AK = "AK";
    [DisplayName("Alberta")]
    public const string AB = "AB";
    [DisplayName("Arkansas")]
    public const string AR = "AR";
    [DisplayName("Arizona")]
    public const string AZ = "AZ";
    [DisplayName("British Columbia")]
    public const string BC = "BC";
    [DisplayName("Colorado")]
    public const string CO = "CO";
    [DisplayName("Connecticut")]
    public const string CT = "CT";
    [DisplayName("Delaware")]
    public const string DE = "DE";
    [DisplayName("East Bay")]
    public const string EB = "EB";
    [DisplayName("Eastern Massachusetts")]
    public const string EMA = "EMA";
    [DisplayName("Eastern New York")]
    public const string ENY = "ENY";
    [DisplayName("Eastern Pennsylvania")]
    public const string EPA = "EPA";
    [DisplayName("Eastern Washington")]
    public const string EWA = "EWA";
    [DisplayName("Georgia")]
    public const string GA = "GA";
    [DisplayName("Idaho")]
    public const string ID = "ID";
    [DisplayName("Illinois")]
    public const string IL = "IL";
    [DisplayName("Indiana")]
    public const string IN = "IN";
    [DisplayName("Iowa")]
    public const string IA = "IA";
    [DisplayName("Kansas")]
    public const string KS = "KS";
    [DisplayName("Kentucky")]
    public const string KY = "KY";
    [DisplayName("Los Angeles")]
    public const string LAX = "LAX";
    [DisplayName("Louisiana")]
    public const string LA = "LA";
    [DisplayName("Maine")]
    public const string ME = "ME";
    [DisplayName("Manitoba")]
    public const string MB = "MB";
    [DisplayName("Maritime")]
    public const string MAR = "MAR";
    [DisplayName("Maryland-DC")]
    public const string MDC = "MDC";
    [DisplayName("Michigan")]
    public const string MI = "MI";
    [DisplayName("Minnesota")]
    public const string MN = "MN";
    [DisplayName("Mississippi")]
    public const string MS = "MS";
    [DisplayName("Missouri")]
    public const string MO = "MO";
    [DisplayName("Montana")]
    public const string MT = "MT";
    [DisplayName("Nebraska")]
    public const string NE = "NE";
    [DisplayName("Nevada")]
    public const string NV = "NV";
    [DisplayName("New Hampshire")]
    public const string NH = "NH";
    [DisplayName("New Mexico")]
    public const string NM = "NM";
    [DisplayName("New York City-Long Island")]
    public const string NLI = "NLI";
    [DisplayName("Newfoundland/Labrador")]
    public const string NL = "NL";
    [DisplayName("North Carolina")]
    public const string NC = "NC";
    [DisplayName("North Dakota")]
    public const string ND = "ND";
    [DisplayName("North Texas")]
    public const string NTX = "NTX";
    [DisplayName("Northern Florida")]
    public const string NFL = "NFL";
    [DisplayName("Northern New Jersey")]
    public const string NNJ = "NNJ";
    [DisplayName("Northern New York")]
    public const string NNY = "NNY";
    [DisplayName("Northwest Territories/Yukon/Nunavut")]
    public const string NT = "NT";
    [DisplayName("Northwest Territories/Yukon/Nunavut (NWT)")]
    [LegacyValue]
    public const string NWT = "NWT";
    [DisplayName("Ohio")]
    public const string OH = "OH";
    [DisplayName("Oklahoma")]
    public const string OK = "OK";
    [DisplayName("Ontario")]
    public const string ON = "ON";
    [DisplayName("Orange")]
    public const string ORG = "ORG";
    [DisplayName("Oregon")]
    public const string OR = "OR";
    [DisplayName("Pacific")]
    public const string PAC = "PAC";
    [DisplayName("Puerto Rico")]
    public const string PR = "PR";
    [DisplayName("Quebec")]
    public const string QC = "QC";
    [DisplayName("Rhode Island")]
    public const string RI = "RI";
    [DisplayName("Sacramento Valley")]
    public const string SV = "SV";
    [DisplayName("San Diego")]
    public const string SDG = "SDG";
    [DisplayName("San Francisco")]
    public const string SF = "SF";
    [DisplayName("San Joaquin Valley")]
    public const string SJV = "SJV";
    [DisplayName("Santa Barbara")]
    public const string SB = "SB";
    [DisplayName("Santa Clara Valley")]
    public const string SCV = "SCV";
    [DisplayName("Saskatchewan")]
    public const string SK = "SK";
    [DisplayName("South Carolina")]
    public const string SC = "SC";
    [DisplayName("South Dakota")]
    public const string SD = "SD";
    [DisplayName("South Texas")]
    public const string STX = "STX";
    [DisplayName("Southern Florida")]
    public const string SFL = "SFL";
    [DisplayName("Southern New Jersey")]
    public const string SNJ = "SNJ";
    [DisplayName("Tennessee")]
    public const string TN = "TN";
    [DisplayName("US Virgin Islands")]
    public const string VI = "VI";
    [DisplayName("Utah")]
    public const string UT = "UT";
    [DisplayName("Vermont")]
    public const string VT = "VT";
    [DisplayName("Virginia")]
    public const string VA = "VA";
    [DisplayName("West Central Florida")]
    public const string WCF = "WCF";
    [DisplayName("West Texas")]
    public const string WTX = "WTX";
    [DisplayName("West Virginia")]
    public const string WV = "WV";
    [DisplayName("Western Massachusetts")]
    public const string WMA = "WMA";
    [DisplayName("Western New York")]
    public const string WNY = "WNY";
    [DisplayName("Western Pennsylvania")]
    public const string WPA = "WPA";
    [DisplayName("Western Washington")]
    public const string WWA = "WWA";
    [DisplayName("Wisconsin")]
    public const string WI = "WI";
    [DisplayName("Wyoming")]
    public const string WY = "WY";

    public static IEnumerable<Option> Get() {

      var options = typeof(ArrlSection).GetValues();

      foreach (var option in options) {
        yield return new Option() { Code = option };
        }
      }

    public static IEnumerable<string> GetValues() {
      return typeof(ArrlSection).GetValues();
      }
    }

  [Enumeration]
  public static class EQslReceivedStatus {

    public const string Yes = "Y";
    public const string No = "N";
    public const string Requested = "R";
    [DisplayName("Ignore/Invalid")]
    public const string IgnoreInvalid = "I";
    public const string Validated = "V";

    }

  [Enumeration]
  public static class BooleanValue {

    [AlternateName("True")]
    public const string Yes = "Y";
    [AlternateName("False")]
    public const string No = "N"; 
    }

  [Enumeration]
  public static class EQslSentStatus {

    public const string Yes = "Y";
    public const string No = "N";
    public const string Requested = "R";
    public const string Queued = "Q";
    [DisplayName("Ignore/Invalid")]
    public const string IgnoreInvalid = "I";
    }

  [Enumeration]
  public static class Continent {

    [DisplayName("North America")]
    public const string NorthAmerica = "NA";
    [DisplayName("South America")]
    public const string SouthAmerica = "SA";
    public const string Europe = "EU";
    public const string Africa = "AF";
    public const string Oceania = "OC";
    public const string Asia = "AS";
    public const string Antarctica = "AN";
    }

  [Enumeration]
  public static class PropagationMode {

    [DisplayName("Aircraft Scatter")]
    public const string AircraftScatter = "AS";
    public const string Aurora = "AUR";
    [DisplayName("Aurora-E")]
    public const string AuroraE = "AUE";
    [DisplayName("Back Scatter")]
    public const string BackScatter = "BS";
    public const string EchoLink = "ECH";
    [DisplayName("Earth-Moon-Earth")]
    public const string EarthMoonEarth = "EME";
    [DisplayName("F2 Reflection")]
    public const string F2Reflection = "F2";
    [DisplayName("Field Aligned Irregularities")]
    public const string FieldAlignedIrregularities = "FAI";
    [DisplayName("Internet-Assisted")]
    public const string InternetAssisted = "INTERNET";
    public const string Ionoscatter = "ION";
    public const string IRLP = "IRL";
    [DisplayName("Meteor Scatter")]
    public const string MeteorScatter = "MS";
    [DisplayName("Terrestrial/Atmospheric Repeater/Transponder")]
    public const string RepeaterTransponder = "RPT";
    [DisplayName("Rain Scatter")]
    public const string RainScatter = "RS";
    public const string Satellite = "SAT";
    [DisplayName("Sporadic-E")]
    public const string SporadicE = "ES";
    [DisplayName("Trans-equatorial")]
    public const string TransEquatorial = "TEP";
    [DisplayName("Tropospheric Ducting")]
    public const string TroposphericDucting = "TR";
    }

  [Enumeration]
  public static class QslMedium {
    public const string Card = "CARD";
    [DisplayName("eQSL.cc")]
    public const string EQsl = "EQSL";
    [DisplayName("Log Book of the World")]
    public const string LogBookOfTheWorld = "LOTW";
    }

  [Enumeration]
  public static class AntennaPath {
    public const string Grayline = "G";
    public const string Short = "S";
    public const string Long = "L";
    public const string Other = "O";
    }

  [Enumeration]
  public static class Via {
    public const string Bureau = "B";
    public const string Direct = "D";
    public const string Electronic = "E";
    [ImportOnly]
    public const string Manager = "M";
    }

  [Enumeration]
  public static class QsoCompleteStatus {

    public const string Yes = "Y";
    public const string No = "N";
    [DisplayName("Not Heard")]
    public const string NotHeard = "NIL";
    public const string Uncertain = "?";
    }

  [Enumeration]
  public static class QsoUploadStatus {

    [DisplayName("Uploaded/Accepted")]
    public const string UploadedAccepted = "Y";
    [DisplayName("Not Uploaded/Do Not Upload")]
    public const string DoNotUpload = "N";
    public const string Modified = "M";
    }

  [Enumeration]
  public static class SponsoredAwardPrefix {

    [DisplayName("ADIF Development Group")]
    public const string ADIF = "ADIF_";
    [DisplayName("ARI - l'Associazione Radioamatori Italiani")]
    public const string ARI = "ARI_";
    [DisplayName("ARRL - American Radio Relay League")]
    public const string ARRL = "ARRL_";
    [DisplayName("CQ Magazine")]
    public const string CQ = "CQ_";
    [DisplayName("DARC - Deutscher Amateur-Radio-Club e.V.")]
    public const string DARC = "DARC_";
    [DisplayName("eQSL.cc")]
    public const string eQSL = "EQSL_";
    [DisplayName("IARU - International Amateur Radio Union")]
    public const string IARU = "IARU_";
    [DisplayName("JARL - Japan Amateur Radio League")]
    public const string JARL = "JARL_";
    [DisplayName("RSGB - Radio Society of Great Britain")]
    public const string RSGB = "RSGB_";
    [DisplayName("TAG - Tambov Award Group")]
    public const string TAG = "TAG_";
    [DisplayName("WAB - Worked all Britain")]
    public const string WAB = "WABAG_";
    }

  [Enumeration]
  public static class Award {

    [ImportOnly]
    public const string AJA = "AJA";
    [ImportOnly]
    public const string CQDX = "CQDX";
    [ImportOnly]
    public const string CQDXFIELD = "CQDXFIELD";
    [ImportOnly]
    public const string CQWAZ_MIXED = "CQWAZ_MIXED";
    [ImportOnly]
    public const string CQWAZ_CW = "CQWAZ_CW";
    [ImportOnly]
    public const string CQWAZ_PHONE = "CQWAZ_PHONE";
    [ImportOnly]
    public const string CQWAZ_RTTY = "CQWAZ_RTTY";
    [ImportOnly]
    public const string CQWAZ_160m = "CQWAZ_160m";
    [ImportOnly]
    public const string CQWPX = "CQWPX";
    [ImportOnly]
    public const string DARC_DOK = "DARC_DOK";
    [ImportOnly]
    public const string DXCC = "DXCC";
    [ImportOnly]
    public const string DXCC_MIXED = "DXCC_MIXED";
    [ImportOnly]
    public const string DXCC_CW = "DXCC_CW";
    [ImportOnly]
    public const string DXCC_PHONE = "DXCC_PHONE";
    [ImportOnly]
    public const string DXCC_RTTY = "DXCC_RTTY";
    [ImportOnly]
    public const string IOTA = "IOTA";
    [ImportOnly]
    public const string JCC = "JCC";
    [ImportOnly]
    public const string JCG = "JCG";
    [ImportOnly]
    public const string MARATHON = "MARATHON";
    [ImportOnly]
    public const string RDA = "RDA";
    [ImportOnly]
    public const string WAB = "WAB";
    [ImportOnly]
    public const string WAC = "WAC";
    [ImportOnly]
    public const string WAE = "WAE";
    [ImportOnly]
    public const string WAIP = "WAIP";
    [ImportOnly]
    public const string WAJA = "WAJA";
    [ImportOnly]
    public const string WAS = "WAS";
    [ImportOnly]
    public const string WAZ = "WAZ";
    [ImportOnly]
    public const string USACA = "USACA";
    [ImportOnly]
    public const string VUCC = "VUCC";
    }

  [Enumeration]
  public class CountryCode {

    public int Code { get; set; }

    public string Name { get; set; }

    public bool Deleted { get; set; }

    public static string[] GetValuesArray() {
      return GetValues()?.ToArray() ?? new string[] { };
      }

    public static IEnumerable<string> GetValues() {
      foreach (var country in Get())
        yield return country.Code.ToString();
      }

    public static IEnumerable<CountryCode> Get() {

      using (var dbHelper = new SQLiteHelper($"{Resources.DatabasePath}{Resources.DatabaseFileName}")) {

        var countries = dbHelper.ReadData(Resources.RetrieveCountryCodesSql);

        foreach (dynamic country in countries) {
          yield return new CountryCode() { Deleted = country.Legacy,
                                           Name = country.Name,
                                           Code = country.Value };
          }
        }
      }

    public static CountryCode Get(int code) {
      return null;
      }

    }

  [Enumeration]
  public class PrimaryAdminSubdivision {

    public int CountryCode { get; set; }

    public string Code {  get; set; }

    public string Name { get; set; }

    public bool Legacy { get; set; }

    public bool ImportOnly { get; set; }

    public string Prefix { get; set; }

    public string AlternateCode { get; set; }

    public string AlternateName { get; set; }


    public static string[] GetOptionsArray() {
      return GetOptions()?.ToArray() ?? new string[] { };
      }

    public static IEnumerable<string> GetOptions() {
      foreach (var division in Get())
        yield return division.Code;
      }

    public static IEnumerable<PrimaryAdminSubdivision> Get() {

      using (var dbHelper = new SQLiteHelper($"{Resources.DatabasePath}{Resources.DatabaseFileName}")) {

        var divisions = dbHelper.ReadData("");

        foreach (dynamic division in divisions) {
          yield return new PrimaryAdminSubdivision() { ImportOnly = division.Legacy,
                                                       Legacy = division.Legacy,
                                                       Name = division.Name,
                                                       Code = division.Value,
                                                       CountryCode = division.CountryCode,
                                                       Prefix = division.Prefix };
          }
        }
      }

    public static IEnumerable<PrimaryAdminSubdivision> Get(int countryCode) {

      using (var dbHelper = new SQLiteHelper($"{Resources.DatabasePath}{Resources.DatabaseFileName}")) {

        var divisions = dbHelper.ReadData("");

        foreach (dynamic division in divisions) {
          yield return new PrimaryAdminSubdivision() { ImportOnly = division.Legacy,
                                                       Legacy = division.Legacy,
                                                       Name = division.Name,
                                                       Code = division.Value,
                                                       CountryCode = division.CountryCode,
                                                       Prefix = division.Prefix };
          }
        }
      }
    }

  [Enumeration]
  public static class Credit {

    public const string CQDX = "CQDX";
    public const string CQDX_BAND = "CQDX_BAND";
    public const string CQDX_MODE = "CQDX_MODE";
    public const string CQDX_MOBILE = "CQDX_MOBILE";
    public const string CQDX_QRP = "CQDX_QRP";
    public const string CQDX_SATELLITE = "CQDX_SATELLITE";
    public const string CQDXFIELD = "CQDXFIELD";
    public const string CQDXFIELD_BAND = "CQDXFIELD_BAND";
    public const string CQDXFIELD_MODE = "CQDXFIELD_MODE";
    public const string CQDXFIELD_MOBILE = "CQDXFIELD_MOBILE";
    public const string CQDXFIELD_QRP = "CQDXFIELD_QRP";
    public const string CQDXFIELD_SATELLITE = "CQDXFIELD_SATELLITE";
    public const string CQWAZ_MIXED = "CQWAZ_MIXED";
    public const string CQWAZ_BAND = "CQWAZ_BAND";
    public const string CQWAZ_MODE = "CQWAZ_MODE";
    public const string CQWAZ_SATELLITE = "CQWAZ_SATELLITE";
    public const string CQWAZ_EME = "CQWAZ_EME";
    public const string CQWAZ_MOBILE = "CQWAZ_MOBILE";
    public const string CQWAZ_QRP = "CQWAZ_QRP";
    public const string CQWPX = "CQWPX";
    public const string CQWPX_BAND = "CQWPX_BAND";
    public const string CQWPX_MODE = "CQWPX_MODE";
    public const string DXCC = "DXCC";
    public const string DXCC_BAND = "DXCC_BAND";
    public const string DXCC_MODE = "DXCC_MODE";
    public const string DXCC_SATELLITE = "DXCC_SATELLITE";
    public const string IOTA = "IOTA";
    public const string RDA = "RDA";
    public const string USACA = "USACA";
    public const string VUCC_BAND = "VUCC_BAND";
    public const string VUCC_SATELLITE = "VUCC_SATELLITE";
    public const string WAB = "WAB";
    public const string WAC = "WAC";
    public const string WAC_BAND = "WAC_BAND";
    public const string WAE = "WAE";
    public const string WAE_BAND = "WAE_BAND";
    public const string WAE_MODE = "WAE_MODE";
    public const string WAIP = "WAIP";
    public const string WAIP_BAND = "WAIP_BAND";
    public const string WAIP_MODE = "WAIP_MODE";
    public const string WAS = "WAS";
    public const string WAS_BAND = "WAS_BAND";
    public const string WAS_MODE = "WAS_MODE";
    public const string WAS_SATELLITE = "WAS_SATELLITE";

    public static IEnumerable<string> Get() {
      return typeof(Credit).GetValues();
      }
    }

  [Enumeration]
  public class Contest {

    public string Name { get; set; }

    public string Value { get; set; }

    public bool Legacy { get; set; }

    public bool ImportOnly { get; set; }

    public static string[] GetOptionsArray() {
      return GetOptions()?.ToArray() ?? new string[] { };
      }

    public static IEnumerable<string> GetOptions() {
      foreach (var contest in Get())
        yield return contest.Value;
      }

    public static IEnumerable<Contest> Get() {
      
      using (var dbHelper = new SQLiteHelper($"{Resources.DatabasePath}{Resources.DatabaseFileName}")) {

        var contests = dbHelper.ReadData(Resources.RetrieveContestsSql);

        foreach (dynamic contest in contests) {
          yield return new Contest() { ImportOnly = contest.ImportOnly,
                                       Legacy = contest.Legacy,
                                       Name = contest.Name,
                                       Value = contest.Value };
          }
        }
      }
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

  [Enumeration]
  public static class Mode {

    public const string AM = "AM";
    public const string ARDOP = "ARDOP";
    public const string ATV = "ATV";
    public const string C4FM = "C4FM";

    [SubOption("CHIP64")]
    [SubOption("CHIP128")]
    public const string CHIP = "CHIP";
    public const string CLO = "CLO";
    public const string CONTESTI = "CONTESTI";

    [SubOption("PCW")]
    public const string CW = "CW";
    public const string DIGITALVOICE = "DIGITALVOICE";

    [SubOption("DOMINOEX")]
    [SubOption("DOMINOF")]
    public const string DOMINO = "DOMINO";
    public const string DSTAR = "DSTAR";
    public const string FAX = "FAX";
    public const string FM = "FM";
    public const string FSK441 = "FSK441";
    public const string FT8 = "FT8";

    [SubOption("FMHELL")]
    [SubOption("FSKHELL")]
    [SubOption("HELL80")]
    [SubOption("HFSK")]
    [SubOption("PSKHELL")]
    public const string HELL = "HELL";

    [SubOption("ISCAT-A")]
    [SubOption("ISCAT-B")]
    public const string ISCAT = "ISCAT";

    [SubOption("JT4A")]
    [SubOption("JT4B")]
    [SubOption("JT4C")]
    [SubOption("JT4D")]
    [SubOption("JT4E")]
    [SubOption("JT4F")]
    [SubOption("JT4G")]
    public const string JT4 = "JT4";
    public const string JT6M = "JT6M";

    [SubOption("JT9-1")]
    [SubOption("JT9-2")]
    [SubOption("JT9-5")]
    [SubOption("JT9-10")]
    [SubOption("JT9-30")]
    [SubOption("JT9A")]
    [SubOption("JT9B")]
    [SubOption("JT9C")]
    [SubOption("JT9D")]
    [SubOption("JT9E")]
    [SubOption("JT9E FAST")]
    [SubOption("JT9F")]
    [SubOption("JT9F FAST")]
    [SubOption("JT9G")]
    [SubOption("JT9G FAST")]
    [SubOption("JT9H")]
    [SubOption("JT9H FAST")]
    public const string JT9 = "JT9";
    public const string JT44 = "JT44";

    [SubOption("JT65A")]
    [SubOption("JT65B")]
    [SubOption("JT65B2")]
    [SubOption("JT65C")]
    [SubOption("JT65C2")]
    public const string JT65 = "JT65";
    [SubOption("FSQCALL")]
    [SubOption("FT4")]
    [SubOption("JS8")]
    [SubOption("MFSK4")]
    [SubOption("MFSK8")]
    [SubOption("MFSK11")]
    [SubOption("MFSK16")]
    [SubOption("MFSK22")]
    [SubOption("MFSK31")]
    [SubOption("MFSK32")]
    [SubOption("MFSK64")]
    [SubOption("MFSK128")]
    public const string MFSK = "MFSK";
    public const string MSK144 = "MSK144";
    public const string MT63 = "MT63";

    [SubOption("OLIVIA 4/125")]
    [SubOption("OLIVIA 4/250")]
    [SubOption("OLIVIA 8/250")]
    [SubOption("OLIVIA 8/500")]
    [SubOption("OLIVIA 16/500")]
    [SubOption("OLIVIA 16/1000")]
    [SubOption("OLIVIA 32/1000")]
    public const string OLIVIA = "OLIVIA";

    [SubOption("OPERA-BEACON")]
    [SubOption("OPERA-QSO")]
    public const string OPERA = "OPERA";

    [SubOption("PAC2")]
    [SubOption("PAC3")]
    [SubOption("PAC4")]
    public const string PAC = "PAC";

    [SubOption("PAX2")]
    public const string PAX = "PAX";
    public const string PKT = "PKT";

    [SubOption("FSK31")]
    [SubOption("PSK10")]
    [SubOption("PSK31")]
    [SubOption("PSK63")]
    [SubOption("PSK63F")]
    [SubOption("PSK125")]
    [SubOption("PSK250")]
    [SubOption("PSK500")]
    [SubOption("PSK1000")]
    [SubOption("PSKAM10")]
    [SubOption("PSKAM31")]
    [SubOption("PSKAM50")]
    [SubOption("PSKFEC31")]
    [SubOption("QPSK31")]
    [SubOption("QPSK63")]
    [SubOption("QPSK125")]
    [SubOption("QPSK250")]
    [SubOption("QPSK500")]
    [SubOption("SIM31")]
    public const string PSK = "PSK";
    public const string PSK2K = "PSK2K";
    public const string Q15 = "Q15";

    [SubOption("QRA64A")]
    [SubOption("QRA64B")]
    [SubOption("QRA64C")]
    [SubOption("QRA64D")]
    [SubOption("QRA64E")]
    public const string QRA64 = "QRA64";

    [SubOption("ROS-EME")]
    [SubOption("ROS-HF")]
    [SubOption("ROS-MF")]
    public const string ROS = "ROS";

    [SubOption("ASCI")]
    public const string RTTY = "RTTY";
    public const string RTTYM = "RTTYM";

    [SubOption("LSB")]
    [SubOption("USB")]
    public const string SSB = "SSB";
    public const string SSTV = "SSTV";
    public const string T10 = "T10";
    public const string THOR = "THOR";

    [SubOption("THRBX")]
    public const string THRB = "THRB";

    [SubOption("AMTORFEC")]
    [SubOption("GTOR")]
    public const string TOR = "TOR";
    public const string V4 = "V4";
    public const string VOI = "VOI";
    public const string WINMOR = "WINMOR";
    public const string WSPR = "WSPR";

    [ImportOnly]
    public const string AMTORFEC = "AMTORFEC";
    [ImportOnly]
    public const string ASCI = "ASCI";
    [ImportOnly]
    public const string CHIP64 = "CHIP64";
    [ImportOnly]
    public const string CHIP128 = "CHIP128";
    [ImportOnly]
    public const string DOMINOF = "DOMINOF";
    [ImportOnly]
    public const string FMHELL = "FMHELL";
    [ImportOnly]
    public const string FSK31 = "FSK31";
    [ImportOnly]
    public const string GTOR = "GTOR";
    [ImportOnly]
    public const string HELL80 = "HELL80";
    [ImportOnly]
    public const string HFSK = "HFSK";
    [ImportOnly]
    public const string JT4A = "JT4A";
    [ImportOnly]
    public const string JT4B = "JT4B";
    [ImportOnly]
    public const string JT4C = "JT4C";
    [ImportOnly]
    public const string JT4D = "JT4D";
    [ImportOnly]
    public const string JT4E = "JT4E";
    [ImportOnly]
    public const string JT4F = "JT4F";
    [ImportOnly]
    public const string JT4G = "JT4G";
    [ImportOnly]
    public const string JT65A = "JT65A";
    [ImportOnly]
    public const string JT65B = "JT65B";
    [ImportOnly]
    public const string JT65C = "JT65C";
    [ImportOnly]
    public const string MFSK8 = "MFSK8";
    [ImportOnly]
    public const string MFSK16 = "MFSK16";
    [ImportOnly]
    public const string PAC2 = "PAC2";
    [ImportOnly]
    public const string PAC3 = "PAC3";
    [ImportOnly]
    public const string PAX2 = "PAX2";
    [ImportOnly]
    public const string PCW = "PCW";
    [ImportOnly]
    public const string PSK10 = "PSK10";
    [ImportOnly]
    public const string PSK31 = "PSK31";
    [ImportOnly]
    public const string PSK63 = "PSK63";
    [ImportOnly]
    public const string PSK63F = "PSK63F";
    [ImportOnly]
    public const string PSK125 = "PSK125";
    [ImportOnly]
    public const string PSKAM10 = "PSKAM10";
    [ImportOnly]
    public const string PSKAM31 = "PSKAM31";
    [ImportOnly]
    public const string PSKAM50 = "PSKAM50";
    [ImportOnly]
    public const string PSKFEC31 = "PSKFEC31";
    [ImportOnly]
    public const string PSKHELL = "PSKHELL";
    [ImportOnly]
    public const string QPSK31 = "QPSK31";
    [ImportOnly]
    public const string QPSK63 = "QPSK63";
    [ImportOnly]
    public const string QPSK125 = "QPSK125";
    [ImportOnly]
    public const string THRBX = "THRBX";
    }

  [Enumeration]
  public static class Band {

    static readonly Dictionary<string, double> LowerFrequencies = 
      new Dictionary<string, double>() {  { TwoThousandOneHundredNinetyMeters, 0.1357 },
                                          { SixHundredThirtyMeters, 0.472 },
                                          { FiveHundredSixtyMeters, 0.501 },
                                          { OneHundredSixtyMeters, 1.8 },
                                          { EightyMeters, 3.5 },
                                          { SixtyMeters, 5.06 },
                                          { FortyMeters, 7.0 },
                                          { ThirtyMeters, 10.1 },
                                          { TwentyMeters, 14.0 },
                                          { SeventeenMeters, 18.068 },
                                          { FifteenMeters, 21.0 },
                                          { TwelveMeters, 24.890 },
                                          { TenMeters, 28.0 },
                                          { SixMeters, 50.0 },
                                          { FourMeters, 70.0 },
                                          { TwoMeters, 144.0 },
                                          { OnePointTwoFiveMeters, 222.0 },
                                          { SeventyCentimeters, 420.0 },
                                          { ThirtyThreeCentimeters, 902.0 },
                                          { TwentyThreeCentimeters, 1240.0 },
                                          { ThirteenCentimeters, 2300.0 },
                                          { NineCentimeters, 3300.0 },
                                          { SixCentimeters, 5650.0 },
                                          { ThreeCentimeters, 10000.0 },
                                          { OnePointTwoFiveCentimeters, 24000.0 },
                                          { SixMillimeters, 47000.0 },
                                          { FourMillimeters, 75500.0 },
                                          { TwoPointFiveMillimeters, 119980.0 },
                                          { TwoMillimeters, 142000.0 },
                                          { OneMillimeter, 241000.0 } };

    // min heap
    static readonly Dictionary<string, double> UpperFrequencies =
      new Dictionary<string, double>() {  { TwoThousandOneHundredNinetyMeters, 0.1378 },
                                          { SixHundredThirtyMeters, 0.479 },
                                          { FiveHundredSixtyMeters, 0.504 },
                                          { OneHundredSixtyMeters, 2.0 },
                                          { EightyMeters, 4.0 },
                                          { SixtyMeters, 5.45 },
                                          { FortyMeters, 7.3 },
                                          { ThirtyMeters, 10.15 },
                                          { TwentyMeters, 14.35 },
                                          { SeventeenMeters, 18.168 },
                                          { FifteenMeters, 21.45 },
                                          { TwelveMeters, 24.99 },
                                          { TenMeters, 29.7 },
                                          { SixMeters, 54.0 },
                                          { FourMeters, 71.0 },
                                          { TwoMeters, 148.0 },
                                          { OnePointTwoFiveMeters, 225.0 },
                                          { SeventyCentimeters, 450.0 },
                                          { ThirtyThreeCentimeters, 928.0 },
                                          { TwentyThreeCentimeters, 1300.0 },
                                          { ThirteenCentimeters, 2450.0 },
                                          { NineCentimeters, 3500.0 },
                                          { SixCentimeters, 5925.0 },
                                          { ThreeCentimeters, 10500.0 },
                                          { OnePointTwoFiveCentimeters, 24250.0 },
                                          { SixMillimeters, 47200.0 },
                                          { FourMillimeters, 81000.0 },
                                          { TwoPointFiveMillimeters, 120020.0 },
                                          { TwoMillimeters, 149000.0 },
                                          { OneMillimeter, 250000.0 } };

    [DisplayName("2190m")]
    public const string TwoThousandOneHundredNinetyMeters = "2190m";
    [DisplayName("630m")]
    public const string SixHundredThirtyMeters = "630m";
    [DisplayName("560m")]
    public const string FiveHundredSixtyMeters = "560m";
    [DisplayName("160m")]
    public const string OneHundredSixtyMeters = "160m";
    [DisplayName("80m")]
    public const string EightyMeters = "80m";
    [DisplayName("60m")]
    public const string SixtyMeters = "60m";
    [DisplayName("2190m")]
    public const string FortyMeters = "40m";
    [DisplayName("30m")]
    public const string ThirtyMeters = "30m";
    [DisplayName("20m")]
    public const string TwentyMeters = "20m";
    [DisplayName("17m")]
    public const string SeventeenMeters = "17m";
    [DisplayName("15m")]
    public const string FifteenMeters = "15m";
    [DisplayName("12m")]
    public const string TwelveMeters = "12m";
    [DisplayName("10m")]
    public const string TenMeters = "10m";
    [DisplayName("6m")]
    public const string SixMeters = "6m";
    [DisplayName("4m")]
    public const string FourMeters = "4m";
    [DisplayName("2m")]
    public const string TwoMeters = "2m";
    [DisplayName("1.25m")]
    public const string OnePointTwoFiveMeters = "1.25m";
    [DisplayName("70cm")]
    public const string SeventyCentimeters = "70cm";
    [DisplayName("33cm")]
    public const string ThirtyThreeCentimeters = "33cm";
    [DisplayName("23cm")]
    public const string TwentyThreeCentimeters = "23cm";
    [DisplayName("13cm")]
    public const string ThirteenCentimeters = "13cm";
    [DisplayName("9cm")]
    public const string NineCentimeters = "9cm";
    [DisplayName("6cm")]
    public const string SixCentimeters = "6cm";
    [DisplayName("3cm")]
    public const string ThreeCentimeters = "3cm";
    [DisplayName("1.25cm")]
    public const string OnePointTwoFiveCentimeters = "1.25cm";
    [DisplayName("6mm")]
    public const string SixMillimeters = "6mm";
    [DisplayName("4mm")]
    public const string FourMillimeters = "4mm";
    [DisplayName("2.5mm")]
    public const string TwoPointFiveMillimeters = "2.5mm";
    [DisplayName("2mm")]
    public const string TwoMillimeters = "2mm";
    [DisplayName("1mm")]
    public const string OneMillimeter = "1mm";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="band"></param>
    /// <returns></returns>
    public static double GetUpperFrequency(string band) {

      band = (band ?? string.Empty).ToLower();

      if (UpperFrequencies.ContainsKey(band))
        return UpperFrequencies[band];

      throw new ArgumentException("Invalid band.");
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="band"></param>
    /// <returns></returns>
    public static double GetLowerFrequency(string band) {

      band = (band ?? string.Empty).ToLower();

      if (LowerFrequencies.ContainsKey(band))
        return LowerFrequencies[band];

      throw new ArgumentException("Invalid band.");
      }

    /// <summary>
    /// Retrieves the band to which the specified frequency belongs.
    /// </summary>
    /// <param name="frequency">Frequency used for determining the band.</param>
    /// <returns>String representation of the band to which the specified frequency belongs.</returns>
    public static string Get(double frequency) {

      foreach (var entry in UpperFrequencies) {
        if (entry.Value >= frequency) {
          return frequency >= GetLowerFrequency(entry.Key) && 
                 frequency <= GetUpperFrequency(entry.Key) ? entry.Key : string.Empty;
          }
        }

      return string.Empty;
      }

    /// <summary>
    /// Determines whether the specified freqency is valid for the specified band.
    /// </summary>
    /// <param name="frequency">Frequency that will be checked for validity.</param>
    /// <param name="band">Band that will be checked for validity.</param>
    /// <returns>True if the frequency is valid for the band, else false.</returns>
    public static bool IsValidFrequency(double frequency, string band) {

      band = (band ?? string.Empty).ToLower();

      var upper = GetUpperFrequency(band);
      var lower = GetLowerFrequency(band);

      return frequency >= lower && frequency <= upper;
      }
    }
  }
