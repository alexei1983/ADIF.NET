using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using ADIF.NET;
using ADIF.NET.Tags;
using ADIF.NET.Helpers;

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

      var file = @"C:\wsjtx_log.adi";
      var parser = new Parser();
      parser.LoadFile(file);
      var res = parser.Parse();
      //var coll = parser.GetQsoCollection();

      //var dxcc = new DxccTag();
      //dxcc.SetValue("1");

      //var band = Band.Get(2.14566333);



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
