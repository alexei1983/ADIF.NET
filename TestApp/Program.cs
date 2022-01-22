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

      var cg = new CreditGrantedTag();
      cg.AddValue("WAS", "CARD");
      cg.AddValue("SOTA");
      cg.AddValue("IOTA", "LOTW");
      cg.AddValue("IOTA", "CARD");
      cg.AddValue("IOTA", "BUR");
      Console.WriteLine(cg.TextValue);

      ADIFCreditList.Parse("IOTA,WAS:LOTW&CARD,DXCC:CARD");

      var p = new Parser();
      p.LoadFile(@"C:\Users\S017138\Desktop\wsjtx_log - Copy.adi");
      var result = p.Parse();


      return;


      var listTag = new List<Dictionary<string, string>>();
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5GHM" }, { "TIME_ON", "2025" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KA7TXS" }, { "TIME_ON", "2026" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6SPK" }, { "TIME_ON", "2028" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5AKB" }, { "TIME_ON", "2029" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC5NX" }, { "TIME_ON", "2030" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KB5ZYC" }, { "TIME_ON", "2033" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1127" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KD9KSR" }, { "TIME_ON", "2035" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AD0WN" }, { "TIME_ON", "2036" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5NXK" }, { "TIME_ON", "2037" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6VIP" }, { "TIME_ON", "2037" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-8325" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N8AJM" }, { "TIME_ON", "2038" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN6KI" }, { "TIME_ON", "2039" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC2VOM" }, { "TIME_ON", "2041" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1587" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W0RMS" }, { "TIME_ON", "2043" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC3PIP" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AC0CU" }, { "TIME_ON", "2044" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KF0CGP" }, { "TIME_ON", "2045" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-9579" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-5640" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7ANK" }, { "TIME_ON", "2050" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4572" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AJ5C" }, { "TIME_ON", "2052" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VA7DBJ" }, { "TIME_ON", "2053" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "VE-3660" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5TAO" }, { "TIME_ON", "2054" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W2GZQ" }, { "TIME_ON", "2057" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3433" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC3SDQ" }, { "TIME_ON", "2059" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N5USD" }, { "TIME_ON", "2101" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KD9HZZ" }, { "TIME_ON", "2103" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "VE7QH" }, { "TIME_ON", "2104" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "VE-0445" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N7TWB" }, { "TIME_ON", "2105" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5CEL" }, { "TIME_ON", "2106" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AB3VK" }, { "TIME_ON", "2103" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N6WHY" }, { "TIME_ON", "2109" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-0210" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AD0BC" }, { "TIME_ON", "2111" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W4TMD" }, { "TIME_ON", "2112" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN4ABC" }, { "TIME_ON", "2113" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5AZO" }, { "TIME_ON", "2113" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N7HDD" }, { "TIME_ON", "2114" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N5HA" }, { "TIME_ON", "2115" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KC0ECQ" }, { "TIME_ON", "2116" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW7G" }, { "TIME_ON", "2117" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5KTD" }, { "TIME_ON", "2118" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W9RFU" }, { "TIME_ON", "2119" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K6BME" }, { "TIME_ON", "2119" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1139" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1EDX" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7PYP" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1EDX" }, { "TIME_ON", "2120" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K7RJ" }, { "TIME_ON", "2123" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KI5KOI" }, { "TIME_ON", "2124" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-0315" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7RTA" }, { "TIME_ON", "2125" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-2843" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WO4ROG" }, { "TIME_ON", "2126" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WI6NG" }, { "TIME_ON", "2126" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3421" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5LDA" }, { "TIME_ON", "2128" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WD8PVS" }, { "TIME_ON", "2129" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N1ATV" }, { "TIME_ON", "2132" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K6TVB" }, { "TIME_ON", "2133" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW2DX" }, { "TIME_ON", "2135" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N1KJS" }, { "TIME_ON", "2136" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N8DGD" }, { "TIME_ON", "2137" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W1CLM" }, { "TIME_ON", "2141" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB8UBR" }, { "TIME_ON", "2143" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB6MPH" }, { "TIME_ON", "2144" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-1204" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5ALE" }, { "TIME_ON", "2145" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KN6KOR" }, { "TIME_ON", "2146" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-7377" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K9TDW" }, { "TIME_ON", "2147" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W6GAE" }, { "TIME_ON", "2148" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5JCC" }, { "TIME_ON", "2149" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE8JKW" }, { "TIME_ON", "2149" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "AA3C" }, { "TIME_ON", "2150" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K4QQG" }, { "TIME_ON", "2151" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KK4DX" }, { "TIME_ON", "2151" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KS9Q" }, { "TIME_ON", "2152" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KW9E" }, { "TIME_ON", "2157" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W5NNI" }, { "TIME_ON", "2158" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3000" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K9WBT" }, { "TIME_ON", "2159" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "KE8RAJ" }, { "TIME_ON", "2204" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "K5KTD" }, { "TIME_ON", "2205" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" } });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W3BAY" }, { "TIME_ON", "2209" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4471" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W3BAY" }, { "TIME_ON", "2209" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-4559" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "N6EF" }, { "TIME_ON", "2214" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3584" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "W7JSJ" }, { "TIME_ON", "2218" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-3261" }  });
      listTag.Add(new Dictionary<string, string>() { { "Band", "20m" }, { "Call", "WB4E" }, { "TIME_ON", "2225" }, { "QSO_DATE", "2022-01-15" },
        { "My_Sig_Info", "K-9615" },  { "Mode", "USB" }, { "Operator", "K0UOG" }, { "SIG_INFO", "K-2964" }  });



      var dataset = new ADIFDataSet(listTag);
      dataset.ToADIF(@"C:\Users\S017138\Desktop\K0UOG@K-9615-20220115.adi", EmitFlags.AddCreatedTimestampIfNotPresent | EmitFlags.AddProgramIdIfNotPresent | EmitFlags.MirrorOperatorAndStationCallSign);

      return;
      var utag = new UserDefValueTag(null);

      var file = @"C:\Users\S017138\Desktop\K0UOG@K-2366-20211222.adi";
      var parser = new Parser();
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


      var str = adif.ToADIF(EmitFlags.LowercaseTagNames | EmitFlags.AddCreatedTimestampIfNotPresent | EmitFlags.AddProgramIdIfNotPresent | EmitFlags.MirrorOperatorAndStationCallSign);

      var countries = ADIFEnumeration.Get("Band");

      //var band = Band.Get(2.14566333);

      var alaskaRegex = @"^P[A-I].{1,}$";

      var is1 = System.Text.RegularExpressions.Regex.IsMatch("XE2TN", alaskaRegex);
      var is2 = System.Text.RegularExpressions.Regex.IsMatch("XX4HG", alaskaRegex);
      var is3 = System.Text.RegularExpressions.Regex.IsMatch("XA4TN", alaskaRegex);
      var is4 = System.Text.RegularExpressions.Regex.IsMatch("N0MTN", alaskaRegex);
      var is5 = System.Text.RegularExpressions.Regex.IsMatch("AL91B", alaskaRegex);

      var band = Band.IsFrequencyInBand("20m", 14.101, 2);

     


      //var loc = new ADIFLocation(locStr);
      //var decDeg = loc.ToDecimalDegrees();

      var myGrid = new GridSquareHelper().GetGridSquareFromCall("K0UOG");

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
