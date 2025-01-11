## ADIF.NET
Fully featured ADIF and ADX parser and generator for .NET

### Introduction
Amateur Data Interchange Format (ADIF) is a standard for exchanging ham radio on-air contacts between different platforms and systems.

The full standard can be found at the ADIF web site.

This library parses and generates files in both ADIF and ADX formats.

### Example Usage
To parse an ADIF file stored on your local machine, instantiate the `AdifParser` class, load the file, and call the `Parse` method:

```
var parser = new AdifParser();
parser.LoadFile(@"C:\Users\username\Desktop\contacts.adi");
var adifData = parser.Parse();
```

To create a new QSO record, instantiate the `AdifQso` class and call methods to apply data to the QSO:

```
var newQso = new AdifQso();
newQso.SetCall("K9BZM");
newQso.SetDateTimeOn(DateTime.UtcNow);
newQso.SetBand("20m");
newQso.SetMode("USB");
newQso.SetOperator("K0UOG");
newQso.SetName("Steve");

Console.WriteLine(newQso.ToString("A"));
```

The above code outputs the following text to the console:

```
<CALL:5>K9BZM <QSO_DATE:8>20250111 <TIME_ON:6>005549 <BAND:3>20m <MODE:3>USB <OPERATOR:5>K0UOG <NAME:5>Steve <EOR>
```

### SQL Adapter
This library also provides the `AdifSqlAdapter` class for interacting with QSO data stored in a database. Any database framework that implements the `System.Data.IDbConnection` and associated interfaces can work 
with the adapter class.

Each QSO is assigned a unique ID (a `Guid` with the dashes removed), which is stored in an application-defined field named `APP_ADIFNET_QSOUNIQID`.

The SQL adapter relies on mapping ADIF tag names to columns in the database, as defined by an instance of the `AdifColumnMappings` class. That class provides two default mappings:

- `AdifColumnMappings.DefaultMinimum`, which maps the CALL, OPERATOR, QSO_DATE, TIME_ON, BAND, and MODE tags to database fields of the same name
- `AdifColumnMappings.All`, which maps all ADIF tags to database fields with the same names as each tag

In both cases, the application-defined tag containing the QSO unique ID is included in the mapping.

To store the `newQso` object in a MySQL database table named `QSOs`, instantiate the adapter as follows:

```
var mysqlConnection = new MySqlConnection("server=localhost;user=user;database=adif;password=password;");

var sqlAdapter = new AdifSqlAdapter(mysqlConnection, "QSOs")
{
	ParameterPrefix = '?',
	ReservedFieldsEscapedBy = ReservedWordEscape.Backticks;
};

sqlAdapter.Insert(newQso);

var qsos = sqlAdapter.RetrieveAll();
```

The newly saved QSO will now be included in the QSO collection stored in the variable `qsos`.

