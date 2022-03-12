using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using ADIF.NET;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;
using ADIF.NET.Types;

namespace TestApp {
  class Program {

    public const int MORSE_NONE = 0x0;
    public static readonly int[] morse_ascii = new int[] {
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    0x73, MORSE_NONE, 0x55, 0x32,                   /* , _ . / */
    0x3F, 0x2F, 0x27, 0x23,                         /* 0 1 2 3 */
    0x21, 0x20, 0x30, 0x38,                         /* 4 5 6 7 */
    0x3C, 0x3E, MORSE_NONE, MORSE_NONE,             /* 8 9 _ _ */
    MORSE_NONE, 0x31, MORSE_NONE, 0x4C,             /* _ = _ ? */
    MORSE_NONE, 0x05, 0x18, 0x1A,                   /* _ A B C */
    0x0C, 0x02, 0x12, 0x0E,                         /* D E F G */
    0x10, 0x04, 0x17, 0x0D,                         /* H I J K */
    0x14, 0x07, 0x06, 0x0F,                         /* L M N O */
    0x16, 0x1D, 0x0A, 0x08,                         /* P Q R S */
    0x03, 0x09, 0x11, 0x0B,                         /* T U V W */
    0x19, 0x1B, 0x1C, MORSE_NONE,                   /* X Y Z _ */
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
    MORSE_NONE, 0x05, 0x18, 0x1A,                   /* _ A B C */
    0x0C, 0x02, 0x12, 0x0E,                         /* D E F G */
    0x10, 0x04, 0x17, 0x0D,                         /* H I J K */
    0x14, 0x07, 0x06, 0x0F,                         /* L M N O */
    0x16, 0x1D, 0x0A, 0x08,                         /* P Q R S */
    0x03, 0x09, 0x11, 0x0B,                         /* T U V W */
    0x19, 0x1B, 0x1C, MORSE_NONE,                   /* X Y Z _ */
    MORSE_NONE, MORSE_NONE, MORSE_NONE, MORSE_NONE,
     };

    static void Main(string[] args) {

      var columnMappings = ADIFColumnMappings.All;
      //columnMappings.Add(new ADIFColumnMapping("MY_AMP", "MY_AMP", true, false));
      //columnMappings.Add(new ADIFColumnMapping("MY_AMP_INTL", "MY_AMP_INTL", true, false));
      //columnMappings.Add(new ADIFColumnMapping("QSO_TRANSCRIPT_INTL", "QSO_TRANSCRIPT_INTL", true, false));
      //columnMappings.Add(new ADIFColumnMapping("QSO_TRANSCRIPT", "QSO_TRANSCRIPT", true, false));
      //columnMappings.Add(new ADIFColumnMapping("STATE", "StateVal"));
      //columnMappings.Add(new ADIFColumnMapping("MY_STATE", "MyStateVal"));
      //columnMappings.Add(new ADIFColumnMapping("ARRL_SECT", "ARRL_SECT"));
      //columnMappings.Add(new ADIFColumnMapping("DXCC"));
      //columnMappings.Add(new ADIFColumnMapping("MY_DXCC"));
      //columnMappings.Add(new ADIFColumnMapping("LAT"));
      //columnMappings.Add(new ADIFColumnMapping("LON"));
      //columnMappings.Add(new ADIFColumnMapping("MY_LAT"));
      //columnMappings.Add(new ADIFColumnMapping("MY_LON"));
      //columnMappings.Add(new ADIFColumnMapping("SIG_INFO"));
      //columnMappings.Add(new ADIFColumnMapping("MY_SIG_INFO"));
      //columnMappings.Add(new ADIFColumnMapping("MY_COUNTRY_INTL"));
      //columnMappings.Add(new ADIFColumnMapping("MY_COUNTRY"));
      //columnMappings.Add(new ADIFColumnMapping("COUNTRY"));
      //columnMappings.Add(new ADIFColumnMapping("COUNTRY_INTL"));


      var parse = new ADXParser();
      parse.LoadFile(@"C:\Users\S017138\Downloads\ADIF_312_released_test_QSOs_2021_04_17\ADIF_312_test_QSOs_2021_04_17.adx");
      var result = parse.Parse();

      var insertList = new List<ADIFQSO>();

      using (var conn = new System.Data.SqlClient.SqlConnection("Server=ddcicetstdb;Database=Ice;Integrated Security=true;"))
      {
        var adapter = new SQLAdapter(conn, "dbo.QSOs", result.Header) { ColumnMappings = columnMappings };
        foreach (var qsoInternal in result.QSOs)
        {
          var retQso = adapter.Insert(qsoInternal);
          if (retQso != null)
            insertList.Add(retQso);
        }

        var qsosNew = adapter.RetrieveByQSODateBetween(new DateTime(2021, 2, 17), new DateTime(2021, 2, 18));
        Console.WriteLine(qsosNew.Count());

      }

      //Console.WriteLine(new ADIFDataSet() { Header = result.Header, QSOs = new ADIFQSOCollection(qsosNew.ToArray()) }.ToString("A"));


      return;
      //var items = typeof(Via).GetValues().ToArray();
      //var optionValues = OptionValue.FromType(typeof(Mode), false, false);

      //var myname = new MyNameTag();
      //myname.SetValue("Alex Jennings");

      //var qsoDate = new QsoDateTag();
      //qsoDate.SetValue(DateTime.Now);

      //var mynametext = myname.Build();
      //var qsodatetext = qsoDate.Build();

      //var g = new ADIF.NET.Helpers.GridSquareHelper();
      //var ersu = g.GetGridSquare("3205 W. Avondale Dr, Denver, CO", ADIF.NET.Helpers.GridSquareHelper.LookupType.Address);

      //var latlng = new Unclassified.Util.LatLng();
      //latlng.Lat = -37.1039;
      //latlng.Long = 103.7414;

      //Console.WriteLine(latlng.ToString());

      var xparser = new ADXParser();
      //xparser.LoadFile(@"C:\Users\S017138\Desktop\K0UOG@K-0226-20220220-4.xml");
     // var resx = xparser.Parse();

     // return;

      var lat = "S033 51.516";
      var lng = "E151 12.850";

      ADIFLocation.TryParse(lat, out Location latLoc );
      if (latLoc != null)
        Console.WriteLine(latLoc.ToDecimalDegrees());

      ADIFLocation.TryParse(lng, out Location lngLoc);
      if (lngLoc != null)
        Console.WriteLine(lngLoc.ToDecimalDegrees());

      Location locLat = ADIFLocation.FromDecimalDegrees(-33.858611m, LocationType.Latitude);
      Location locLng = ADIFLocation.FromDecimalDegrees(151.214167m, LocationType.Longitude);

      Console.WriteLine();
      Console.WriteLine(locLat.ToString());
      Console.WriteLine(locLng.ToString());

      var cg = new CreditGrantedTag();
      cg.AddValue("WAS", "CARD");
      cg.AddValue("SOTA");
      cg.AddValue("IOTA", "LOTW");
      cg.AddValue("IOTA", "CARD");
      cg.AddValue("IOTA", "BUR");
      Console.WriteLine(cg.TextValue);

      Console.WriteLine(DXCCHelper.CountryHasPrimarySubdivision(227));

      Console.WriteLine(DXCCHelper.ValidatePrimarySubdivision(227, "99"));

      Console.WriteLine(DXCCHelper.ValidateSecondarySubdivision(6, "AK", "AK,Aleutians East"));

      Console.WriteLine(DXCCHelper.PrimarySubdivisionHasSecondarySubdivision(50, "AK"));


      var credits = Values.Credits;

      //var parse = new ADXParser();
      //parse.LoadFile(@"C:\Users\S017138\Downloads\ADIF_312_released_test_QSOs_2021_04_17\ADIF_312_test_QSOs_2021_04_17.adx");
      //parse.LoadFile(@"C:\Users\S017138\Desktop\K0UOG@K-0225-20220212.adi");
      //var result = parse.Parse();

      //result.Header.Add(new ADIFVersionTag(new Version(3, 1, 2)));
      //result.AddQSOTag(new MyNameTag("Alex"));
      //result.AddQSOTag(new MySigTag("POTA"));
      //result.AddQSOTag(new MySigInfoTag("K-0225"));
      //result.QSOs[0].SetRstSent(5, 9);
      //result.QSOs[0].SetRstRcvd(5, 3);

      //result.QSOs[0].SetRstSent(5, 9);
      //result.QSOs[0].SetRstRcvd(5, 3);

      result.CheckVersion();

      var dataSet2 = new ADIFDataSet();

      var qso2 = new ADIFQSO();
      qso2.SetCall("W7GZA");
      qso2.SetOperator("K0UOG");
      qso2.SetDateTimeOn(DateTime.UtcNow);
      qso2.SetBand("20m");
      qso2.SetMode("USB");

      var qso3 = new ADIFQSO();
      qso3.SetCall("W7GZA");
      qso3.SetOperator("K0UOG");
      qso3.SetDateTimeOn(DateTime.UtcNow);
      qso3.SetBand("20m");
      qso3.SetMode("USB");

      dataSet2.QSOs.Add(qso2);
      dataSet2.QSOs.Add(qso3);








      //dataSet2.AddQSOTag(new NameTag("Alex"));

      //var qso1 = new ADIFQSO();
      //qso1.AddName("Bob");
      //qso1.SetQSODateTimeOn(DateTime.UtcNow);
      //qso1.SetRstRcvd(5, 8);
      //qso1.SetRstSent(5, 5);
      //qso1.SetMode("USB");
      //qso1.SetCall("W7GZP");
      //qso1.SetOperator("K0UOG");
      //qso1.SetMyAddress("14561 E Ford Pl Unit 15", "Aurora", "CO", "80012", "United States", "291");
      //qso1.SetMyEquipment("Yaesu FT-450D", "Alpha magloop 80m-10m", 100);
      //qso1.SetFreq(14.320, true);
      //qso1.SetComment("Bob is cool.");
      //qso1.SetEquipment("ICOM 7100", 100);
      //qso1.SetSummitToSummit("W6/NS-0011", "W0C/FR-1099");
      //qso1.SetSatellite("Mother Ship", "K");
      //qso1.SetQRZUploaded(DateTime.UtcNow, "Y");
      //qso1.MarkEQSLSent(DateTime.UtcNow, true);
      
      //dataSet2.AddQSOTag(new MyNameTag("Alex"));
      //var user1 = dataSet2.AddUserDefinedTagDefinition("My_Birthday", "D");
      //var user2 = dataSet2.AddUserDefinedTagDefinition("Gift_Options", "Amazon", "B&N", "HRO", "Gigaparts");
      //var user3 = dataSet2.AddUserDefinedTagDefinition("Spending_Limits", 25, 150);

      //var qso2 = new ADIFQSO();
      //qso2.AddName("Tom");
      //qso2.SetQSODateTimeOn(DateTime.UtcNow);
      //qso2.SetRstRcvd(3, 3);
      //qso2.SetRstSent(5, 2);
      //qso2.SetMode("USB");
      //qso2.SetCall("W7GZP");
      //qso2.SetOperator("K0UOG");
      //qso2.SetMyEquipment("Yaesu FT-450D", "Alpha magloop 80m-10m", 100);
      //qso1.SetEquipment("Yaesu FT-891", 10);
      //qso2.SetFreq(14.3255, true);
      //qso2.SetComment("Tom is also cool.");
      //qso2.SetHRDUploaded(DateTime.UtcNow, "Y");
      //qso2.MarkEQSLSent(DateTime.UtcNow, true);
      //qso2.AddUserDefinedTag(user1, new DateTime(1983, 3, 6));
      //qso2.AddUserDefinedTag(user2, "Gigaparts");
      //qso2.AddUserDefinedTag(user3, 100);


      //Console.WriteLine(qso1.IsMatch(qso2));

      //dataSet2.QSOs.Add(qso1);
      //dataSet2.QSOs.Add(qso2);

      //Console.WriteLine(dataSet2.ToADX());




      return;

      //result.ToADIF(@"C:\Users\S017138\Desktop\testadx2adif.adi", EmitFlags.None);
      ///return;

      var qso12 = new ADIFQSO();
      qso12.AddCall("W7ZCO");
      qso12.AddOperator("K0UOG");
      qso12.AddBand("30m");
      qso12.AddFreq(14.347);


      var listTag = new List<Dictionary<string, string>>();
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WE4DX" }, { "TIME_ON", "2130" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "SIG_INFO", "K-4838" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N0YO" }, { "TIME_ON", "2136" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "55" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "NL7V" }, { "TIME_ON", "2138" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "31" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AL7KC" }, { "TIME_ON", "2140" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "32" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AJ5C" }, { "TIME_ON", "2143" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "55" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7GGA" }, { "TIME_ON", "2143" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "54" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KB0MPV" }, { "TIME_ON", "2144" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "53" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5FKN" }, { "TIME_ON", "2147" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "51" },});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KB0PVB" }, { "TIME_ON", "2207" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "57" }, { "SIG_INFO", "K-0226" }});
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7RTA" }, { "TIME_ON", "2219" }, { "QSO_DATE", "2022-03-01" },
          { "Mode", "USB" }, { "Operator", "K0UOG" },  { "RST_RCVD", "44" }, { "SIG_INFO", "K-4576" }});

      var K0041 = new ADIFDataSet(listTag);
      K0041.ToADIF(@"C:\Users\S017138\Desktop\K0UOG@K-0041-20220301.adi", EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      listTag.Clear();

      return;


      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K0KPS" }, { "TIME_ON", "1948" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" } ,{ "RstRcvd", "57" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KK6JJZ" }, { "TIME_ON", "1949" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KG6B" }, { "TIME_ON", "1950" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SOTARef", "W6/NS-0420" }, { "Sig", "SOTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N6GR" }, { "TIME_ON", "1951" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KJ7PRS" }, { "TIME_ON", "1953" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "33" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KI7JOM" }, { "TIME_ON", "1954" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" }, { "Sig_Info", "K-3188" }, { "Sig", "POTA" } });

      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7DFL" }, { "TIME_ON", "2003" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WD5AFR" }, { "TIME_ON", "2005" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "54" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "NL7V" }, { "TIME_ON", "2012" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "31" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KJ6TSX" }, { "TIME_ON", "2016" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "58" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KK7BTT" }, { "TIME_ON", "2020" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "44" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5E" }, { "TIME_ON", "2021" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW6SDW" }, { "TIME_ON", "2021" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "59" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N7PIB" }, { "TIME_ON", "2022" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "59" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "CU3HY" }, { "TIME_ON", "2025" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "50" } });

      var k1130 = new ADIFDataSet(listTag);
      k1130.ToADIF(@"C:\Users\S017138\Desktop\K0UOG@K-1130-20220227-4.xml", EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      listTag.Clear();

      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AK4MK" }, { "TIME_ON", "2123" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "59" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KD7ZPP" }, { "TIME_ON", "2128" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "54" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WD5AFR" }, { "TIME_ON", "2129" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "57" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "NL7V" }, { "TIME_ON", "2131" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "22" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Call", "KK4OMJ" }, { "TIME_ON", "2140" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "37" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Call", "W8BI" }, { "TIME_ON", "2142" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "59" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Call", "KB8UEY" }, { "TIME_ON", "2142" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "59" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Call", "WJ8Y" }, { "TIME_ON", "2148" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "44" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7DBJ" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "52" }, { "sIG_iNFO", "VE-4069" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KJ7OKW" }, { "TIME_ON", "2214" }, { "QSO_DATE", "2022-02-27" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "53" }, { "sIG_iNFO", "K-3596" }, { "sIG", "POTA" } });

      var k3568 = new ADIFDataSet(listTag);
      k3568.ToADIF(@"C:\Users\S017138\Desktop\K0UOG@K-3568-20220227-4.xml", EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      listTag.Clear();

      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7DBJ" }, { "TIME_ON", "0000" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "33" }, { "sIG_iNFO", "VE-3660" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KJ7UIO" }, { "TIME_ON", "0001" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" }, { "sIG_iNFO", "K-0580" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5CYY" }, { "TIME_ON", "0004" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" }, { "sIG_iNFO", "K-0493" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7VZ" }, { "TIME_ON", "0008" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "53" }, { "sIG_iNFO", "VE-0079" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7VZ" }, { "TIME_ON", "0008" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" }, { "sIG_iNFO", "VE-0060" }, { "sIG", "POTA" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KJ7BJS" }, { "TIME_ON", "0010" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WF5TV" }, { "TIME_ON", "0014" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB6KSU" }, { "TIME_ON", "0016" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "57" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW2DX" }, { "TIME_ON", "0020" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "44" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "NL7V" }, { "TIME_ON", "0021" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "31" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KH6PE" }, { "TIME_ON", "0033" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KI5NEQ" }, { "TIME_ON", "0036" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "42" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AJ5ZX" }, { "TIME_ON", "0040" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE4DIS" }, { "TIME_ON", "0041" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "40m" }, { "Call", "K7PRA" }, { "TIME_ON", "0054" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "54" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "40m" }, { "Call", "AK6DM" }, { "TIME_ON", "0055" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "57" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "40m" }, { "Call", "K6REH" }, { "TIME_ON", "0056" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "55" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "40m" }, { "Call", "K7ZZQ" }, { "TIME_ON", "0106" }, { "QSO_DATE", "2022-02-28" },
          { "Mode", "USB" }, { "Operator", "K0UOG" }, { "RstRcvd", "44" }, { "SIG_INFO", "K-3062" }, { "SIG", "POTA" } });

      var k3526 = new ADIFDataSet(listTag);
      k3526.ToADIF(@"C:\Users\S017138\Desktop\K0UOG@K-3526-20220227-4.xml", EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      return;
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KF0EXJ" }, { "TIME_ON", "2033" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-0177" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE5XV" }, { "TIME_ON", "2040" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-6602" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KF0BQH" }, { "TIME_ON", "2042" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-0370" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB0RUR" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-9161" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K8XJ" }, { "TIME_ON", "2048" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-1996" } });

      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.350" }, { "Call", "N7NWP" }, { "TIME_ON", "2051" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-9561" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.355" }, { "Call", "KD8JJF" }, { "TIME_ON", "2057" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-9483" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8APS" }, { "TIME_ON", "2104" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-6706" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "K8UHF" }, { "TIME_ON", "2104" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "K4QQG" }, { "TIME_ON", "2105" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "KV4NI" }, { "TIME_ON", "2106" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "N3XLS" }, { "TIME_ON", "2110" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8NFM" }, { "TIME_ON", "2112" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8RBW" }, { "TIME_ON", "2113" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "N4SAX" }, { "TIME_ON", "2114" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "K8IKO" }, { "TIME_ON", "2118" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "AA8LF" }, { "TIME_ON", "2119" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "KO4GAR" }, { "TIME_ON", "2121" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "AB8LL" }, { "TIME_ON", "2122" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8GMF" }, { "TIME_ON", "2124" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "K4MMC" }, { "TIME_ON", "2124" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "KI6ZLW" }, { "TIME_ON", "2128" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8TGR" }, { "TIME_ON", "2128" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "N8MMB" }, { "TIME_ON", "2129" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "KG4WAS" }, { "TIME_ON", "2129" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W4WDK" }, { "TIME_ON", "2130" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "N4TIZ" }, { "TIME_ON", "2131" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W8GSR" }, { "TIME_ON", "2132" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "W4PGM" }, { "TIME_ON", "2134" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.365" }, { "Call", "KB3CMT" }, { "TIME_ON", "2137" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-4564" } });


      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KI5QAT" }, { "TIME_ON", "2156" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-1059" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KB0RIF" }, { "TIME_ON", "2158" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KJ7DT" }, { "TIME_ON", "2158" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "AJ5C" }, { "TIME_ON", "2158" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K5GHM" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KI5PPY" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W5TJL" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "NK7L" }, { "TIME_ON", "2200" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KD0PYG" }, { "TIME_ON", "2201" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "WA7JGO" }, { "TIME_ON", "2201" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KN6KI" }, { "TIME_ON", "2201" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W7NNR" }, { "TIME_ON", "2202" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KC0ZVN" }, { "TIME_ON", "2202" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KI7UXT" }, { "TIME_ON", "2203" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "N5MCY" }, { "TIME_ON", "2204" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KF6MUY" }, { "TIME_ON", "2204" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KV0RE" }, { "TIME_ON", "2205" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KC3MVS" }, { "TIME_ON", "2205" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KA0LDG" }, { "TIME_ON", "2206" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KI5FXK" }, { "TIME_ON", "2207" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "N5ANY" }, { "TIME_ON", "2207" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "WY6R" }, { "TIME_ON", "2208" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "AD0BC" }, { "TIME_ON", "2208" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KL7TC" }, { "TIME_ON", "2208" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KD2VSE" }, { "TIME_ON", "2210" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K4WES" }, { "TIME_ON", "2210" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W6HRH" }, { "TIME_ON", "2211" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "VA3GTN" }, { "TIME_ON", "2212" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KR7KN" }, { "TIME_ON", "2213" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-3202" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W4JHO" }, { "TIME_ON", "2213" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KD9RPE" }, { "TIME_ON", "2214" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KJ7BPN" }, { "TIME_ON", "2214" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K6REH" }, { "TIME_ON", "2215" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "WF5TV" }, { "TIME_ON", "2215" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KC0ECQ" }, { "TIME_ON", "2216" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-1755" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "AC5WD" }, { "TIME_ON", "2216" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "NZ2S" }, { "TIME_ON", "2217" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K2DMS" }, { "TIME_ON", "2218" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KB6QEW" }, { "TIME_ON", "2218" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K0AMY" }, { "TIME_ON", "2219" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "WB2HNL" }, { "TIME_ON", "2219" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K5RAR" }, { "TIME_ON", "2219" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KC2BPP" }, { "TIME_ON", "2220" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K4CAE" }, { "TIME_ON", "2221" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KD9DZT" }, { "TIME_ON", "2222" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KE4WLL" }, { "TIME_ON", "2222" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W0CCR" }, { "TIME_ON", "2224" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "K6EXE" }, { "TIME_ON", "2226" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "N2HOQ" }, { "TIME_ON", "2226" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "VE7QH" }, { "TIME_ON", "2244" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "VE-4061" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "N9GSU" }, { "TIME_ON", "2300" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-6977" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "W6VIP" }, { "TIME_ON", "2307" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-8325" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KD2VQE" }, { "TIME_ON", "2309" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-1347" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "N1SPW" }, { "TIME_ON", "2316" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-3559" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.336" }, { "Call", "KK5HT" }, { "TIME_ON", "2325" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-3059" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "2m" }, { "Freq", "146.52" }, { "Call", "AC0FT" }, { "TIME_ON", "2333" }, { "QSO_DATE", "2022-02-20" },
      //    { "Mode", "FM" }, { "Operator", "K0UOG" }  });





      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.316" }, { "Call", "KI5JWS" }, { "TIME_ON", "2028" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-0493" } });

      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "K7SEN" }, { "TIME_ON", "2029" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "KJ7PRS" }, { "TIME_ON", "2030" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "KD9ROB" }, { "TIME_ON", "2031" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "KC9HEQ" }, { "TIME_ON", "2031" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "N6WHY" }, { "TIME_ON", "2032" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" },  { "Sig_Info", "K-1137" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "N6QIR" }, { "TIME_ON", "2032" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "KF0AIT" }, { "TIME_ON", "2033" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-0367" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "K7GDR" }, { "TIME_ON", "2034" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-6434" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "W8JDE" }, { "TIME_ON", "2035" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "NN6EE" }, { "TIME_ON", "2036" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "K4AFN" }, { "TIME_ON", "2040" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-3532" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "W6JLV" }, { "TIME_ON", "2042" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" }, { "Sig_Info", "K-0058" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "WA2EDJ" }, { "TIME_ON", "2043" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Freq", "14.335" }, { "Call", "N7MWH" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });

      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.325" }, { "Call", "KN6KI" }, { "TIME_ON", "2053" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "15m" }, { "Freq", "21.325" }, { "Call", "KI5LKS" }, { "TIME_ON", "2054" }, { "QSO_DATE", "2022-02-12" },
      //    { "Mode", "USB" }, { "Operator", "K0UOG" } });










      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KA7TXS" }, { "TIME_ON", "2026" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6SPK" }, { "TIME_ON", "2028" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5AKB" }, { "TIME_ON", "2029" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5NX" }, { "TIME_ON", "2030" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KB5ZYC" }, { "TIME_ON", "2033" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1127" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KD9KSR" }, { "TIME_ON", "2035" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AD0WN" }, { "TIME_ON", "2036" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5NXK" }, { "TIME_ON", "2037" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6VIP" }, { "TIME_ON", "2037" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-8325" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N8AJM" }, { "TIME_ON", "2038" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN6KI" }, { "TIME_ON", "2039" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC2VOM" }, { "TIME_ON", "2041" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1587" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W0RMS" }, { "TIME_ON", "2043" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC3PIP" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AC0CU" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KF0CGP" }, { "TIME_ON", "2045" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-9579" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-5640" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4572" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AJ5C" }, { "TIME_ON", "2052" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7DBJ" }, { "TIME_ON", "2053" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "VE-3660" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5TAO" }, { "TIME_ON", "2054" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W2GZQ" }, { "TIME_ON", "2057" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3433" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC3SDQ" }, { "TIME_ON", "2059" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N5USD" }, { "TIME_ON", "2101" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KD9HZZ" }, { "TIME_ON", "2103" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VE7QH" }, { "TIME_ON", "2104" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "VE-0445" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N7TWB" }, { "TIME_ON", "2105" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5CEL" }, { "TIME_ON", "2106" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AB3VK" }, { "TIME_ON", "2103" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N6WHY" }, { "TIME_ON", "2109" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-0210" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AD0BC" }, { "TIME_ON", "2111" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W4TMD" }, { "TIME_ON", "2112" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN4ABC" }, { "TIME_ON", "2113" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5AZO" }, { "TIME_ON", "2113" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N7HDD" }, { "TIME_ON", "2114" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N5HA" }, { "TIME_ON", "2115" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC0ECQ" }, { "TIME_ON", "2116" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW7G" }, { "TIME_ON", "2117" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5KTD" }, { "TIME_ON", "2118" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W9RFU" }, { "TIME_ON", "2119" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K6BME" }, { "TIME_ON", "2119" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1139" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1EDX" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7PYP" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1EDX" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7RJ" }, { "TIME_ON", "2123" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KI5KOI" }, { "TIME_ON", "2124" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-0315" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7RTA" }, { "TIME_ON", "2125" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-2843" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WO4ROG" }, { "TIME_ON", "2126" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WI6NG" }, { "TIME_ON", "2126" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3421" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5LDA" }, { "TIME_ON", "2128" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WD8PVS" }, { "TIME_ON", "2129" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N1ATV" }, { "TIME_ON", "2132" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K6TVB" }, { "TIME_ON", "2133" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW2DX" }, { "TIME_ON", "2135" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N1KJS" }, { "TIME_ON", "2136" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N8DGD" }, { "TIME_ON", "2137" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1CLM" }, { "TIME_ON", "2141" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB8UBR" }, { "TIME_ON", "2143" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB6MPH" }, { "TIME_ON", "2144" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1204" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5ALE" }, { "TIME_ON", "2145" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN6KOR" }, { "TIME_ON", "2146" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-7377" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K9TDW" }, { "TIME_ON", "2147" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6GAE" }, { "TIME_ON", "2148" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5JCC" }, { "TIME_ON", "2149" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE8JKW" }, { "TIME_ON", "2149" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AA3C" }, { "TIME_ON", "2150" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K4QQG" }, { "TIME_ON", "2151" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KK4DX" }, { "TIME_ON", "2151" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KS9Q" }, { "TIME_ON", "2152" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW9E" }, { "TIME_ON", "2157" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5NNI" }, { "TIME_ON", "2158" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3000" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K9WBT" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE8RAJ" }, { "TIME_ON", "2204" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5KTD" }, { "TIME_ON", "2205" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W3BAY" }, { "TIME_ON", "2209" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4471" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W3BAY" }, { "TIME_ON", "2209" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4559" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N6EF" }, { "TIME_ON", "2214" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3584" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7JSJ" }, { "TIME_ON", "2218" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3261" }  });
      //listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB4E" }, { "TIME_ON", "2225" }, { "QSO_DATE", "2022-01-15" },
      //  { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-2964" }  });



      var dataset = new ADIFDataSet(listTag);

      var timeOn = dataset.QSOs[0].GetQSODateTimeOn();
      dataset.AddQSOTag(new MySigInfoTag("K-0226"));
      dataset.AddQSOTag(new MySigTag("POTA"));
     for(var q = 0; q <dataset.QSOs.Count; q++)
      {
        if(dataset.QSOs[q].Contains("Sig_Info"))
          dataset.QSOs[q].Add(new SigTag("POTA"));
      }
      dataset.AddQSOTag(new MyDXCCTag("291"));
      dataset.AddQSOTag(new MyStateTag("CO"));
      dataset.AddQSOTag(new MyCntyTag("CO,Adams"));
      dataset.AddQSOTag(new MyGridSquareTag("DM79nu"));
      dataset.AddQSOTag(new MyLatTag(ADIFLocation.FromDecimalDegrees((decimal)39.8367, LocationType.Latitude).ToString()));
      dataset.AddQSOTag(new MyLonTag(ADIFLocation.FromDecimalDegrees((decimal)-104.837, LocationType.Longitude).ToString()));

      dataset.ToADX(@"C:\Users\S017138\Desktop\K0UOG@K-0226-20220220-4.xml", EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      return;
      var utag = new UserDefValueTag(null);

      var file = @"C:\Users\S017138\Desktop\K0UOG@K-2366-20211222.adi";
      var parser = new ADIFParser();
      parser.LoadFile(file);
      var res = parser.Parse();
      //var coll = parser.GetQsoCollection();

      var callTag = new CallTag();
      callTag.SetValue("N7LV");

      var bandTag = new BandTag();
      bandTag.SetValue("20m");

      var opTag = new OperatorTag();
      opTag.SetValue("K0UOG");

      var modeTag = new ModeTag();
      modeTag.SetValue("CW");

      var qso = new ADIFQSO(callTag, bandTag, opTag, modeTag, new EndRecordTag());
      var adif = new ADIFDataSet();
      adif.QSOs = new ADIFQSOCollection();
      adif.QSOs.Add(qso);


      var str = adif.ToADIF(EmitFlags.LowercaseTagNames | EmitFlags.AddCreatedTimestamp | EmitFlags.AddProgramHeaderTags | EmitFlags.MirrorOperatorAndStationCallSign);

      var countries = ADIFEnumeration.Get("Band");

      //var band = Band.Get(2.14566333);

      var alaskaRegex = @"^P[A-I].{1,}$";

      var is1 = System.Text.RegularExpressions.Regex.IsMatch("XE2TN", alaskaRegex);
      var is2 = System.Text.RegularExpressions.Regex.IsMatch("XX4HG", alaskaRegex);
      var is3 = System.Text.RegularExpressions.Regex.IsMatch("XA4TN", alaskaRegex);
      var is4 = System.Text.RegularExpressions.Regex.IsMatch("N0MTN", alaskaRegex);
      var is5 = System.Text.RegularExpressions.Regex.IsMatch("AL91B", alaskaRegex);

      var band = Band.IsFrequencyInBand("20m", 14.101);

     


      //var loc = new ADIFLocation(locStr);
      //var decDeg = loc.ToDecimalDegrees();

      //var myGrid = new GridSquareHelper().GetGridSquareFromCall("K0UOG");

      //var r1 ="NA-009".IsSotaDesignator();
      //var r2 = "AN-999".IsSotaDesignator();
      //var r3 = "W0C/FR-001".IsSotaDesignator();
      //var r4 = "G/LP-009".IsSotaDesignator();
      //var r5 = "LZ/RO-008".IsSotaDesignator();

      //var primarysubs = OptionValue.FromDatabase(@"C:\adif.db", Resources.RetrieveCountryCodesSql);

      //var sqlHelper = new SQLiteHelper(@"C:\adif.db", true, false);

      //sqlHelper.DropTable("PrimaryAdminSubdivisions");
      //sqlHelper.ExecuteNonQuery("create table Contests (ContestId varchar(75) not null primary key, ContestName varchar(255) not null, Deprecated bit not null, ValidStart real, ValidEnd real);");
      //sqlHelper.ExecuteNonQuery("update PrimaryAdminSubdivisions set Deprecated = 0");
      //sqlHelper.ExecuteNonQuery("Alter table PrimaryAdminSubdivisions alter column Deprecated bit not null");
      //sqlHelper.ExecuteNonQuery("Alter table PrimaryAdminSubdivisions add AlternateCodeName varchar(150)");
      //sqlHelper.ExecuteNonQuery("Alter table PrimaryAdminSubdivisions add ValidUntil int");
      //sqlHelper.ExecuteNonQuery("update PrimaryAdminSubdivisions set Id = rowid");

      //sqlHelper.ExecuteNonQuery(@"
      //");

      //sqlHelper.ExecuteNonQuery("update PrimaryAdminSubdivisions set Id = rowid");
      //sqlHelper.ExecuteNonQuery("update PrimaryAdminSubdivisions set Prefix = NULL WHERE Prefix = ''");
      //sqlHelper.ExecuteNonQuery("update PrimaryAdminSubdivisions set Deprecated = 0 WHERE Deprecated IS NULL");
      //var data = sqlHelper.ReadData("SELECT * FROM PrimaryAdminSubdivisions");

      //for (var x = 0; x < parser.QsoCount; x++) {
      //  var qso = parser.GetQso(x);

      //  if (qso != null)
      //    Console.WriteLine(qso.ToKeyPairString());
      //  }
      }
    }
  }
