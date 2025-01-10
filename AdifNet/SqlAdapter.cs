using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using org.goodspace.Data.Radio.Adif.Exceptions;
using org.goodspace.Data.Radio.Adif.Tags;

namespace org.goodspace.Data.Radio.Adif
{
    /// <summary>
    /// Determines how reserved words are escaped in a specific database implementation.
    /// </summary>
    public enum ReservedWordEscape
    {
        /// <summary>
        /// Reserved words are escaped by brackets ([])
        /// </summary>
        Brackets,

        /// <summary>
        /// Reserved words are escaped by double quotes (")
        /// </summary>
        DoubleQuotes,

        /// <summary>
        /// Reserved words are escaped by single quotes (')
        /// </summary>
        SingleQuotes,

        /// <summary>
        /// Reserved words are escaped by backticks (`)
        /// </summary>
        Backticks
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlAdapter : IDisposable
    {

        /// <summary>
        /// Database connection.
        /// </summary>
        public IDbConnection Connection { get; private set; }

        /// <summary>
        /// Name of the database table containing the ADIF QSOs.
        /// </summary>
        public string QSOTable { get; set; }

        /// <summary>
        /// Determines how reserved words in SQL are escaped.
        /// </summary>
        public ReservedWordEscape ReservedFieldsEscapedBy { get; set; } = ReservedWordEscape.Brackets;

        /// <summary>
        /// Character that denotes the start of a database parameter.
        /// </summary>
        public char ParameterPrefix { get; set; } = '@';

        /// <summary>
        /// ADIF header containing any relevant user-defined fields stored in the database.
        /// </summary>
        public AdifHeader Header { get; set; }

        /// <summary>
        /// Mapping of ADIF tag names to database columns.
        /// </summary>
        public AdifColumnMappings ColumnMappings
        {
            get => mapping ?? AdifColumnMappings.DefaultMinimum;

            set => mapping = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SqlAdapter"/> class.
        /// </summary>
        /// <param name="connection">Database connection.</param>
        /// <param name="qsoTable">Name of the database table containing the ADIF QSOs.</param>
        /// <param name="header">ADIF header containing any relevant user-defined fields stored in the database.</param>
        public SqlAdapter(IDbConnection connection, string qsoTable, AdifHeader header)
        {
            if (string.IsNullOrEmpty(qsoTable))
                throw new ArgumentException("QSO table name is required.", nameof(qsoTable));

            Connection = connection ?? throw new ArgumentNullException(nameof(connection), "SQL connection is required.");
            QSOTable = qsoTable;

            Connect();

            Header = header ?? [];
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SqlAdapter"/> class.
        /// </summary>
        /// <param name="connection">Database connection.</param>
        /// <param name="qsoTable">Name of the database table containing the ADIF QSOs.</param>
        public SqlAdapter(IDbConnection connection, string qsoTable) : this(connection, qsoTable, []) { }

        /// <summary>
        /// Retrieves all QSOs from the database.
        /// </summary>
        public IEnumerable<AdifQso> RetrieveAll()
        {
            using var command = Connection.CreateCommand();
            command.Connection = Connection;
            command.CommandText = string.Format(SQL_SELECT_COMMAND_TEXT, QSOTable);

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Inserts the specified QSO into the database.
        /// </summary>
        /// <param name="qso">QSO to insert.</param>
        public AdifQso Insert(AdifQso qso)
        {
            if (qso == null)
                throw new ArgumentNullException(nameof(qso), "QSO cannot be null.");

            if (qso.Count < 1)
                return qso;

            Connect();

            using (var command = Connection.CreateCommand())
            {
                var commandText = $"INSERT INTO {QSOTable} (";
                var parameterText = string.Empty;

                var uniqueId = qso.GetTagValue<string>(UNIQ_ID_APP_DEF_FIELD);

                // generate unique ID for the QSO
                if (string.IsNullOrEmpty(uniqueId))
                {
                    uniqueId = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    qso.AddAppDefinedField(UNIQ_ID_APP_DEF_FIELD_NAME, Values.DEFAULT_PROGRAM_ID, uniqueId);
                }

                commandText += $"{EscapeColumnName(UNIQ_ID_SQL_COL)},";
                parameterText += $"{ParameterPrefix}{UNIQ_ID_SQL_COL},";
                command.Parameters.Add(GetParameter(command, UNIQ_ID_SQL_COL, uniqueId));

                foreach (var tag in qso)
                {
                    if (UNIQ_ID_APP_DEF_FIELD.Equals(tag.Name, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var columnNameMapping = ColumnMappings.GetColumnMappingFromTagName(tag.Name);

                    if (columnNameMapping == null || string.IsNullOrEmpty(columnNameMapping.ColumnName))
                        continue;

                    commandText += $"{EscapeColumnName(columnNameMapping.ColumnName)},";
                    parameterText += $"{ParameterPrefix}{columnNameMapping.ColumnName},";
                    command.Parameters.Add(GetParameter(command, tag, columnNameMapping));
                }

                command.CommandText = $"{commandText.Trim(',')}) VALUES ({parameterText.Trim(',')})";

                if (command.ExecuteNonQuery() < 1)
                    throw new Exception("Could not insert QSO!");
            }

            return qso;
        }

        /// <summary>
        /// Updates the specified QSO in the database.
        /// </summary>
        /// <param name="qso">QSO to update.</param>
        public bool Update(AdifQso qso)
        {
            if (qso == null)
                throw new ArgumentNullException(nameof(qso), "QSO cannot be null.");

            if (qso.Count < 1)
                return false;

            var uniqueId = qso.GetTagValue<string>(UNIQ_ID_APP_DEF_FIELD);

            if (string.IsNullOrEmpty(uniqueId))
                throw new Exception("Cannot update QSO: no unique ID found.");

            Connect();

            using (var command = Connection.CreateCommand())
            {
                var commandText = $"UPDATE {QSOTable} SET ";

                foreach (var tag in qso)
                {
                    if (UNIQ_ID_APP_DEF_FIELD.Equals(tag.Name, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var columnNameMapping = ColumnMappings.GetColumnMappingFromTagName(tag.Name);

                    if (columnNameMapping == null || string.IsNullOrEmpty(columnNameMapping.ColumnName))
                        continue;

                    commandText += $"{EscapeColumnName(columnNameMapping.ColumnName)} = {ParameterPrefix}{columnNameMapping.ColumnName},";
                    command.Parameters.Add(GetParameter(command, tag, columnNameMapping));
                }

                commandText = commandText.Trim(',');
                command.CommandText = $"{commandText} WHERE {EscapeColumnName(UNIQ_ID_SQL_COL)} = {ParameterPrefix}{UNIQ_ID_SQL_COL}";
                command.Parameters.Add(GetParameter(command, UNIQ_ID_SQL_COL, uniqueId));

                if (command.Parameters.Count > 1)
                    return command.ExecuteNonQuery() > 0;
            }

            return false;
        }

        /// <summary>
        /// Deletes the specified QSO from the database.
        /// </summary>
        /// <param name="qso">QSO to delete.</param>
        public bool Delete(AdifQso qso)
        {
            if (qso == null)
                throw new ArgumentNullException(nameof(qso), "QSO cannot be null.");

            if (qso.Count < 1)
                return false;

            var uniqueId = qso.GetTagValue<string>(UNIQ_ID_APP_DEF_FIELD);

            if (string.IsNullOrEmpty(uniqueId))
                throw new Exception("Cannot delete QSO: no unique ID found.");

            Connect();

            using (var command = Connection.CreateCommand())
            {
                command.CommandText = $"DELETE FROM {QSOTable} WHERE {EscapeColumnName(UNIQ_ID_SQL_COL)} = {ParameterPrefix}{UNIQ_ID_SQL_COL}";
                command.Parameters.Add(GetParameter(command, UNIQ_ID_SQL_COL, uniqueId));

                if (command.Parameters.Count > 0)
                    return command.ExecuteNonQuery() > 0;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qsos"></param>
        public IEnumerable<AdifQso> Insert(params AdifQso[] qsos)
        {
            if (qsos != null)
            {
                foreach (var qso in qsos)
                    yield return Insert(qso);
            }
        }

        //public static DataTable CreateTable<T>()
        //{
        //  //
        //  var entType = typeof(T);

        //  // set the data table name as class name
        //  var tbl = new DataTable(entType.Name);

        //  // get the property list
        //  var properties = TypeDescriptor.GetProperties(entType);
        //  foreach (PropertyDescriptor prop in properties)
        //  {
        //    //
        //    var colType = prop.PropertyType;
        //    if ((colType.IsGenericType))
        //    {
        //      colType = colType.GetGenericArguments()[0];
        //    }

        //    // add each property as column of the table
        //    tbl.Columns.Add(prop.Name, colType);
        //  }

        //  return tbl;
        //}

        //public static DataTable ConvertTo<T>(IEnumerable<T> lst)
        //{

        //  // create a data table structure based on the collection data type
        //  var tbl = CreateTable<T>();
        //  var entType = typeof(T);

        //  var properties = TypeDescriptor.GetProperties(entType);

        //  // get the list item and add into the list
        //  foreach (var item in lst)
        //  {
        //    var row = tbl.NewRow();
        //    foreach (PropertyDescriptor prop in properties)
        //    {
        //      //
        //      var propertyValue = prop.GetValue(item) ?? DBNull.Value;
        //      row[prop.Name] = propertyValue;
        //    }

        //    tbl.Rows.Add(row);
        //  }

        //  return tbl;
        //}

        #region Generic Retrieve Methods

        /// <summary>
        /// Retrieves QSOs from the database using the specified SQL query text.
        /// </summary>
        /// <param name="sqlQuery">SQL query text to execute.</param>
        /// <param name="parameters">Database parameters.</param>
        public IEnumerable<AdifQso> RetrieveByQuery(string sqlQuery, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sqlQuery))
                throw new ArgumentException("SQL query is required.", nameof(sqlQuery));

            using var command = Connection.CreateCommand();
            command.CommandText = sqlQuery;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter != null)
                        command.Parameters.Add(parameter);
                }
            }

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified SQL query text.
        /// </summary>
        /// <param name="sqlQuery">SQL query text to execute.</param>
        public IEnumerable<AdifQso> RetrieveByQuery(string sqlQuery)
        {
            return RetrieveByQuery(sqlQuery, []);
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database column is equal to 
        /// the specified value.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="searchValue">Value to compare for equality.</param>
        public IEnumerable<AdifQso> RetrieveByMappingEquals(AdifColumnMapping columnMapping, object searchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (searchValue is null)
                throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} = {ParameterPrefix}{columnMapping.ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mappings where the database columns are equal to the 
        /// specified values. The values are combined together using the AND logical operator.
        /// </summary>
        /// <param name="columnMappings">Mapping of ADIF tag names to database columns.</param>
        /// <param name="searchValues">Values to compare for equality.</param>
        public IEnumerable<AdifQso> RetrieveByMappingEquals(AdifColumnMapping[] columnMappings, object[] searchValues)
        {
            if (columnMappings == null || columnMappings.Length < 1)
                throw new ArgumentException("At least one column mapping is required.", nameof(columnMappings));

            if (searchValues == null || searchValues.Length < 1)
                throw new ArgumentException("At least one search value is required.", nameof(searchValues));

            if (searchValues.Length != columnMappings.Length)
                throw new ArgumentException($"Found {columnMappings.Length} column mapping(s) and {searchValues.Length} search value(s): counts must be equal.");

            foreach (var columnMapping in columnMappings)
            {
                if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                    throw new Exception("Invalid column mapping specified.");
            }

            foreach (var searchValue in searchValues)
            {
                if (searchValue is null)
                    throw new Exception("Search value cannot be null.");
            }

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMappings[0].ColumnName)} = {ParameterPrefix}{columnMappings[0].ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMappings[0].ColumnName, searchValues[0]));

            for (var c = 1; c < columnMappings.Length; c++)
            {
                command.CommandText = $"{command.CommandText} AND {EscapeColumnName(columnMappings[c].ColumnName)} = {ParameterPrefix}{columnMappings[c].ColumnName}";
                command.Parameters.Add(GetParameter(command, columnMappings[c].ColumnName, searchValues[c]));
            }

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database column is greater than
        /// the specified value.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="searchValue">Value to compare.</param>
        public IEnumerable<AdifQso> RetrieveByMappingGreaterThan(AdifColumnMapping columnMapping, object searchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (searchValue is null)
                throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} > {ParameterPrefix}{columnMapping.ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database column is less than  
        /// the specified value.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="searchValue">Value to compare.</param>
        public IEnumerable<AdifQso> RetrieveByMappingLessThan(AdifColumnMapping columnMapping, object searchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (searchValue is null)
                throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} < {ParameterPrefix}{columnMapping.ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database column is greater than 
        /// or equal to the specified value.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="searchValue">Value to compare.</param>
        public IEnumerable<AdifQso> RetrieveByMappingGreaterThanOrEqualTo(AdifColumnMapping columnMapping, object searchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (searchValue is null)
                throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} >= {ParameterPrefix}{columnMapping.ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database column is less than 
        /// or equal to the specified value.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="searchValue">Value to compare.</param>
        public IEnumerable<AdifQso> RetrieveByMappingLessThanOrEqualTo(AdifColumnMapping columnMapping, object searchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (searchValue is null)
                throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} <= {ParameterPrefix}{columnMapping.ColumnName}";
            command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        /// <summary>
        /// Retrieves QSOs from the database using the specified mapping where the database value is between 
        /// the two specified values.
        /// </summary>
        /// <param name="columnMapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="startSearchValue">First value to compare.</param>
        /// <param name="endSearchValue">Second value to compare.</param>
        public IEnumerable<AdifQso> RetrieveByMappingBetween(AdifColumnMapping columnMapping, object startSearchValue, object endSearchValue)
        {
            if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
                throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

            if (startSearchValue is null)
                throw new ArgumentNullException(nameof(startSearchValue), "Starting search value is required to retrieve data.");

            if (endSearchValue is null)
                throw new ArgumentNullException(nameof(endSearchValue), "Ending search value is required to retrieve data.");

            using var command = Connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {QSOTable} WHERE {EscapeColumnName(columnMapping.ColumnName)} BETWEEN {ParameterPrefix}{columnMapping.ColumnName}1 AND {ParameterPrefix}{columnMapping.ColumnName}2";
            command.Parameters.Add(GetParameter(command, $"{columnMapping.ColumnName}1", startSearchValue));
            command.Parameters.Add(GetParameter(command, $"{columnMapping.ColumnName}2", endSearchValue));

            using var reader = command.ExecuteReader();
            while (reader != null && reader.Read())
            {
                var qso = GetQsoFromReader(reader);

                if (qso.Count > 0)
                    yield return qso;
            }
        }

        #endregion

        /// <summary>
        /// Retrieves all QSOs from the database for the specified callsign.
        /// </summary>
        /// <param name="call">Callsign.</param>
        public IEnumerable<AdifQso> RetrieveByCall(string call)
        {
            if (string.IsNullOrEmpty(call))
                throw new ArgumentException("Callsign is required.", nameof(call));

            var callMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Call);

            if (callMapping == null || string.IsNullOrEmpty(callMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Call}");

            return RetrieveByMappingEquals(callMapping, call);
        }

        /// <summary>
        /// Retrieves all QSOs from the database for the specified operator callsign.
        /// </summary>
        /// <param name="operatorCall">Operator callsign.</param>
        public IEnumerable<AdifQso> RetrieveByOperator(string operatorCall)
        {
            if (string.IsNullOrEmpty(operatorCall))
                throw new ArgumentException("Operator callsign is required.", nameof(operatorCall));

            var operMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Operator);

            if (operMapping == null || string.IsNullOrEmpty(operMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Operator}");

            return RetrieveByMappingEquals(operMapping, operatorCall);
        }

        /// <summary>
        /// Retrieves all QSOs from the database that occurred between the specified start and end dates.
        /// </summary>
        /// <param name="qsoStartDate">QSO starting date.</param>
        /// <param name="qsoEndDate">QSO ending date.</param>
        public IEnumerable<AdifQso> RetrieveByQSODateBetween(DateTime qsoStartDate, DateTime qsoEndDate)
        {
            if (qsoStartDate < SQL_MIN_DATE_TIME)
                throw new ArgumentException("Invalid QSO starting date.", nameof(qsoStartDate));

            if (qsoEndDate < SQL_MIN_DATE_TIME)
                throw new ArgumentException("Invalid QSO ending date.", nameof(qsoEndDate));

            if (qsoStartDate > qsoEndDate)
                throw new ArgumentException("QSO starting date cannot be greater than QSO ending date.");

            var dateMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.QsoDate);

            if (dateMapping == null || string.IsNullOrEmpty(dateMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.QsoDate}");

            return RetrieveByMappingBetween(dateMapping, qsoStartDate.Date, qsoEndDate.Date);
        }

        /// <summary>
        /// Retrieves all QSOs for the specified <paramref name="band"/> from the database.
        /// </summary>
        /// <param name="band">Amateur radio band.</param>
        public IEnumerable<AdifQso> RetrieveByBand(string band)
        {
            if (string.IsNullOrEmpty(band))
                throw new ArgumentException("Band is required.", nameof(band));

            if (!Values.Bands.IsValid(band))
                throw new ArgumentException($"Invalid band: {band}", nameof(band));

            var bandMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Band);

            if (bandMapping == null || string.IsNullOrEmpty(bandMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Band}");

            return RetrieveByMappingEquals(bandMapping, band);
        }

        /// <summary>
        /// Retrieves all QSOs for the specified <paramref name="mode"/> from the database.
        /// </summary>
        /// <param name="mode">Amateur radio mode.</param>
        public IEnumerable<AdifQso> RetrieveByMode(string mode)
        {
            if (string.IsNullOrEmpty(mode))
                throw new ArgumentException("Mode is required.", nameof(mode));

            if (!Values.Modes.IsValid(mode))
                throw new ArgumentException($"Invalid mode: {mode}", nameof(mode));

            var modeMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Band);

            if (modeMapping == null || string.IsNullOrEmpty(modeMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Mode}");

            return RetrieveByMappingEquals(modeMapping, mode);
        }

        /// <summary>
        /// Retrieves all QSOs for the specified <paramref name="band"/> with the specified 
        /// <paramref name="mode"/> from the database.
        /// </summary>
        /// <param name="band">Amateur radio band.</param>
        /// <param name="mode">Amateur radio mode.</param>
        public IEnumerable<AdifQso> RetrieveByBandMode(string band, string mode)
        {
            if (string.IsNullOrEmpty(band))
                throw new ArgumentException("Band is required.", nameof(band));

            if (!Values.Bands.IsValid(band))
                throw new ArgumentException($"Invalid band: {band}", nameof(band));

            if (string.IsNullOrEmpty(mode))
                throw new ArgumentException("Mode is required.", nameof(mode));

            if (!Values.Modes.IsValid(mode))
                throw new ArgumentException($"Invalid mode: {mode}", nameof(mode));

            var modeMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Band);

            if (modeMapping == null || string.IsNullOrEmpty(modeMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Mode}");

            var bandMapping = ColumnMappings.GetColumnMappingFromTagName(AdifTags.Band);

            if (bandMapping == null || string.IsNullOrEmpty(bandMapping.ColumnName))
                throw new Exception($"No mapping found for tag {AdifTags.Band}");

            return RetrieveByMappingEquals([bandMapping, modeMapping], [band, mode]);
        }

        /// <summary>
        /// Creates a new <see cref="AdifQso"/> using the contents of the specified data <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">Data reader containing QSO data.</param>
        AdifQso GetQsoFromReader(IDataReader reader)
        {
            var qso = new AdifQso();

            if (reader != null && !reader.IsClosed)
            {
                for (var r = 0; r < reader.FieldCount; r++)
                {
                    if (reader.IsDBNull(r))
                        continue;

                    var dbFieldName = reader.GetName(r);

                    if (string.IsNullOrEmpty(dbFieldName))
                        continue;

                    if (UNIQ_ID_SQL_COL.Equals(dbFieldName, StringComparison.OrdinalIgnoreCase))
                        qso.AddAppDefinedField(UNIQ_ID_APP_DEF_FIELD_NAME, Values.DEFAULT_PROGRAM_ID, DataTypes.String, reader.GetValue(r));
                    else
                    {
                        var columnMapping = ColumnMappings.GetColumnMappingFromColumnName(dbFieldName);

                        if (columnMapping == null || string.IsNullOrEmpty(columnMapping.TagName))
                            continue;

                        if (columnMapping.IsUserDef)
                        {
                            if (Header == null)
                                continue;

                            var userDefDefTag = Header.GetUserDefinedTag(columnMapping.TagName) ?? 
                                throw new UserDefTagException($"No user-defined field found with name '{columnMapping.TagName}'.", columnMapping.TagName);

                            qso.AddUserDefinedTag(userDefDefTag, reader.GetValue(r));
                        }
                        else if (columnMapping.IsAppDef)
                        {
                            qso.Add(new AppDefTag(columnMapping.TagName, reader.GetValue(r)));
                        }
                        else
                        {
                            var tag = TagFactory.TagFromName(columnMapping.TagName) ?? throw new Exception($"Invalid ADIF tag name: {columnMapping.TagName}");
                            if (tag.Header)
                                throw new Exception($"Tag {columnMapping.TagName} is a header tag and cannot be added to a QSO.");

                            tag.SetValue(reader.GetValue(r));
                            qso.Add(tag);
                        }
                    }
                }
            }
            return qso;
        }

        bool IsColumnNameEscaped(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentException("Database column name is required.", nameof(columnName));

            var escapeChars = GetReservedWordsEscapedBy();

            char startChar = escapeChars[0];
            char endChar = escapeChars.Length > 1 ? escapeChars[1] : escapeChars[0];

            if (!columnName.StartsWith(startChar))
                return false;

            if (!columnName.EndsWith(endChar))
                return false;

            string[] colParts;

            if (columnName.Contains('.'))
                colParts = columnName.Split('.');
            else
                colParts = [columnName];

            foreach (var part in colParts)
            {
                if (!part.StartsWith(startChar))
                    return false;

                if (!part.EndsWith(endChar))
                    return false;                
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        char[] GetReservedWordsEscapedBy()
        {
            return ReservedFieldsEscapedBy switch
            {
                ReservedWordEscape.Brackets => ['[', ']'],
                ReservedWordEscape.DoubleQuotes => ['"'],
                ReservedWordEscape.SingleQuotes => ['\''],
                ReservedWordEscape.Backticks => ['`'],
                _ => throw new Exception(),
            };
        }

        /// <summary>
        /// Escapes the specified SQL column name.
        /// </summary>
        /// <param name="columnName">Database column name.</param>
        string EscapeColumnName(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentException("Database column name is required.", nameof(columnName));

            if (IsColumnNameEscaped(columnName))
                return columnName;

            string[] colParts;

            if (columnName.Contains('.'))
                colParts = columnName.Split('.');
            else
                colParts = [columnName];

            var escapeChars = GetReservedWordsEscapedBy();

            char startChar = escapeChars[0];
            char endChar = escapeChars.Length > 1 ? escapeChars[1] : escapeChars[0];
            var val = string.Empty;

            foreach (var part in colParts)
            {
                if (!string.IsNullOrEmpty(val) && colParts.Length > 1)
                    val += '.';
                val += $"{startChar}{part}{endChar}";
            }

            return val;
        }

        /// <summary>
        /// Prepares the specified <paramref name="tag"/> for use as a database parameter value using 
        /// the specified <paramref name="mapping"/>.
        /// </summary>
        /// <param name="tag">ADIF tag whose value will be used in the database parameter.</param>
        /// <param name="mapping">Mapping of ADIF tag name to database column.</param>
        object? GetParameterValue(ITag tag, AdifColumnMapping mapping)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag), "Cannot get parameter value: missing ADIF tag.");

            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping), "Cannot get parameter value: missing column mapping.");

            if (mapping.IsRequired && !tag.HasValue())
                throw new Exception($"Tag {tag.Name} is marked as required but has no value.");

            return tag.Value != null ? GetParameterValue(tag.Value) : null;
        }

        /// <summary>
        /// Prepares the specified <paramref name="value"/> for use as a database parameter value.
        /// </summary>
        /// <param name="value">Value to prepare.</param>
        object GetParameterValue(object? value)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime < SQL_MIN_DATE_TIME)
                {
                    dateTime = new DateTime(SQL_MIN_DATE_TIME.Year, SQL_MIN_DATE_TIME.Month, SQL_MIN_DATE_TIME.Day,
                                            dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);
                }

                return dateTime;
            }

            return value is null || (value is string strVal && string.IsNullOrEmpty(strVal)) ? DBNull.Value : value;
        }


        /// <summary>
        /// Creates a database parameter for the specified ADIF <paramref name="tag"/> using the
        /// specified <paramref name="mapping"/>.
        /// </summary>
        /// <param name="command">Database command for which the parameter will be generated.</param>
        /// <param name="tag">ADIF tag for the parameter.</param>
        /// <param name="mapping">Mapping of ADIF tag name to database column.</param>
        /// <param name="direction">Direction of the parameter.</param>
        IDbDataParameter GetParameter(IDbCommand command,
                                      ITag tag,
                                      AdifColumnMapping mapping,
                                      ParameterDirection direction = ParameterDirection.Input)
        {

            if (command == null)
                throw new ArgumentNullException(nameof(command), "DB command is required.");

            var parameter = command.CreateParameter();
            parameter.ParameterName = $"{ParameterPrefix}{mapping.ColumnName}";
            parameter.Value = GetParameterValue(tag, mapping);
            parameter.Direction = direction;
            return parameter;
        }

        /// <summary>
        /// Creates a database parameter with the specified name and value.
        /// </summary>
        /// <param name="command">Database command for which the parameter will be generated.</param>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <param name="direction">Direction of the parameter.</param>
        IDbDataParameter GetParameter(IDbCommand command,
                                      string name,
                                      object value,
                                      ParameterDirection direction = ParameterDirection.Input)
        {

            if (command == null)
                throw new ArgumentNullException(nameof(command), "DB command is required.");

            var parameter = command.CreateParameter();
            parameter.ParameterName = $"{ParameterPrefix}{name}";
            parameter.Value = GetParameterValue(value);
            parameter.Direction = direction;
            return parameter;
        }

        /// <summary>
        /// Validates the state of the database connection and connects if needed.
        /// </summary>
        void Connect()
        {
            if (Connection == null)
                throw new ArgumentNullException(nameof(Connection), "SQL connection cannot be null.");

            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    Connection.Dispose();
                }

                QSOTable = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        const string UNIQ_ID_APP_DEF_FIELD = AdifTags.AppDef + Values.DEFAULT_PROGRAM_ID + "_" + UNIQ_ID_APP_DEF_FIELD_NAME;
        const string UNIQ_ID_APP_DEF_FIELD_NAME = "QSOUNIQID";
        const string UNIQ_ID_SQL_COL = "ADIFNET_UNIQ_ID";
        const string SQL_INSERT_COMMAND_TEXT = "INSERT INTO {0} ({1}) VALUES ({2})";
        const string SQL_SELECT_COMMAND_TEXT = "SELECT * FROM {0}";
        static readonly DateTime SQL_MIN_DATE_TIME = new(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        AdifColumnMappings mapping = [];
    }

    /// <summary>
    /// Contains the ADIF-to-database mappings for QSOs.
    /// </summary>
    public class AdifColumnMappings : List<AdifColumnMapping>
    {
        /// <summary>
        /// Minimum set of ADIF tags mapped to a database.
        /// </summary>
        public static readonly AdifColumnMappings DefaultMinimum = new(new AdifColumnMapping(AdifTags.Call),
                                                                       new AdifColumnMapping(AdifTags.Operator),
                                                                       new AdifColumnMapping(AdifTags.QsoDate),
                                                                       new AdifColumnMapping(AdifTags.TimeOn),
                                                                       new AdifColumnMapping(AdifTags.Band),
                                                                       new AdifColumnMapping(AdifTags.Mode));

        /// <summary>
        /// All standard ADIF tags mapped to a database.
        /// </summary>
        public static readonly AdifColumnMappings All = new([..GetAllMappings()]);

        /// <summary>
        /// Creates a new instance of the <see cref="AdifColumnMappings"/> class.
        /// </summary>
        public AdifColumnMappings() { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifColumnMappings"/> class.
        /// </summary>
        /// <param name="mappings">Mappings to add.</param>
        public AdifColumnMappings(params AdifColumnMapping[] mappings)
        {
            if (mappings != null)
                foreach (var mapping in mappings)
                    Add(mapping);
        }

        /// <summary>
        /// Retrieves the database column name for the specified ADIF tag name.
        /// </summary>
        /// <param name="tagName">ADIF tag name.</param>
        public string? GetColumnName(string tagName)
        {
            return GetColumnMappingFromTagName(tagName)?.ColumnName;
        }

        /// <summary>
        /// Retrieves the ADIF tag name for the specified database column name.
        /// </summary>
        /// <param name="columnName">Database column name.</param>
        public string? GetTagName(string columnName)
        {
            return GetColumnMappingFromColumnName(columnName)?.TagName;
        }

        /// <summary>
        /// Retrieves the mapping for the specified ADIF tag name.
        /// </summary>
        /// <param name="tagName">ADIF tag name.</param>
        public AdifColumnMapping? GetColumnMappingFromTagName(string tagName)
        {
            return this.FirstOrDefault(c => c.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves the mapping for the specified database column name.
        /// </summary>
        /// <param name="columnName">Database column name.</param>
        public AdifColumnMapping? GetColumnMappingFromColumnName(string columnName)
        {
            return this.FirstOrDefault(c => c.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Retrieves all standard ADIF tag names with associated database mapping.
        /// </summary>
        static IEnumerable<AdifColumnMapping> GetAllMappings()
        {
            foreach (var tagName in AdifTags.GetQsoTagNames())
                yield return new AdifColumnMapping(tagName);
        }
    }

    /// <summary>
    /// Contains the mapping for a single ADIF tag name and database column.
    /// </summary>
    public class AdifColumnMapping
    {
        /// <summary>
        /// ADIF tag name.
        /// </summary>
        public string TagName { get; }

        /// <summary>
        /// Database column name.
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// Whether or not the mapping represents a user-defined QSO tag.
        /// </summary>
        public bool IsUserDef { get; }

        /// <summary>
        /// Whether or not the mapping represents an application-defined QSO tag.
        /// </summary>
        public bool IsAppDef { get; }

        /// <summary>
        /// Whether or not the QSO tag is required to be present and have a value.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifColumnMapping"/> class.
        /// </summary>
        /// <param name="tagName">ADIF tag name.</param>
        /// <param name="columnName">Database column name.</param>
        /// <param name="isUserDef">Whether or not the mapping represents a user-defined QSO tag.</param>
        /// <param name="isAppDef">Whether or not the mapping represents an application-defined QSO tag.</param>
        public AdifColumnMapping(string tagName, string columnName, bool isUserDef, bool isAppDef)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("Tag name cannot be null or empty string.", nameof(tagName));

            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentException("Column name cannot be null or empty string.", nameof(columnName));

            if (isUserDef && isAppDef)
                throw new ArgumentException("Mapping cannot represent both an app-defined field and a user-defined field.");

            if (!AdifTags.IsTagName(tagName) && !isUserDef && !isAppDef)
                throw new ArgumentException($"Tag '{tagName}' is not valid.", nameof(tagName));

            IsUserDef = isUserDef;
            IsAppDef = isAppDef;
            TagName = tagName;
            ColumnName = columnName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifColumnMapping"/> class.
        /// </summary>
        /// <param name="tagName">ADIF tag name.</param>
        public AdifColumnMapping(string tagName) : this(tagName, tagName, false, false) { }

        /// <summary>
        /// Creates a new instance of the <see cref="AdifColumnMapping"/> class.
        /// </summary>
        /// <param name="tagName">ADIF tag name.</param>
        /// <param name="columnName">Database column name.</param>
        public AdifColumnMapping(string tagName, string columnName) : this(tagName, columnName, false, false) { }
    }
}
