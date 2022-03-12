using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// 
  /// </summary>
  public class SQLAdapter : IDisposable {

    /// <summary>
    /// 
    /// </summary>
    public IDbConnection Connection { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string QSOTable { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public char ParameterPrefix { get; set; } = '@';

    /// <summary>
    /// 
    /// </summary>
    public ADIFHeader Header { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ADIFColumnMappings ColumnMappings { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="qsoTable"></param>
    public SQLAdapter(IDbConnection connection, string qsoTable, ADIFHeader header)
    {
      if (string.IsNullOrEmpty(qsoTable))
        throw new ArgumentException("QSO table name is required.", nameof(qsoTable));

      Connection = connection ?? throw new ArgumentNullException(nameof(connection), "SQL connection is required.");
      QSOTable = qsoTable;

      Connect();

      if (header != null)
        Header = header;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="qsoTable"></param>
    public SQLAdapter(IDbConnection connection, string qsoTable) : this(connection, qsoTable, null) { }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<ADIFQSO> RetrieveAll()
    {
      using (var command = Connection.CreateCommand())
      {
        command.Connection = Connection;
        command.CommandText = string.Format(SQL_SELECT_COMMAND_TEXT, QSOTable);

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// Inserts a QSO into the database.
    /// </summary>
    /// <param name="qso">QSO to insert.</param>
    public ADIFQSO Insert(ADIFQSO qso)
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

        commandText += $"{UNIQ_ID_SQL_COL},";
        parameterText += $"{ParameterPrefix}{UNIQ_ID_SQL_COL},";
        command.Parameters.Add(GetParameter(command, UNIQ_ID_SQL_COL, uniqueId));

        foreach (var tag in qso)
        {
          if (UNIQ_ID_APP_DEF_FIELD.Equals(tag.Name, StringComparison.OrdinalIgnoreCase))
            continue;

          var columnNameMapping = ColumnMappings.GetColumnMappingFromTagName(tag.Name);

          if (columnNameMapping == null || string.IsNullOrEmpty(columnNameMapping.ColumnName))
            continue;

          commandText += $"{columnNameMapping.ColumnName},";
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
    public bool Update(ADIFQSO qso)
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

          commandText += $"{columnNameMapping.ColumnName} = {ParameterPrefix}{columnNameMapping.ColumnName},";
          command.Parameters.Add(GetParameter(command, tag, columnNameMapping));
        }

        commandText = commandText.Trim(',');
        command.CommandText = $"{commandText} WHERE {UNIQ_ID_SQL_COL} = {ParameterPrefix}{UNIQ_ID_SQL_COL}";
        command.Parameters.Add(GetParameter(command, UNIQ_ID_SQL_COL, uniqueId));

        if (command.Parameters.Count > 1)
          return command.ExecuteNonQuery() > 0;
      }

      return false;
    }

    #region Generic Retrieve Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="searchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingEquals(ADIFColumnMapping columnMapping, object searchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (searchValue is null)
        throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} = {ParameterPrefix}{columnMapping.ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMappings"></param>
    /// <param name="searchValues"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingEquals(ADIFColumnMapping[] columnMappings, object[] searchValues)
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

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMappings[0].ColumnName} = {ParameterPrefix}{columnMappings[0].ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMappings[0].ColumnName, searchValues[0]));

        for (var c = 1; c < columnMappings.Length; c++)
        {
          command.CommandText = $"{command.CommandText} AND {columnMappings[c].ColumnName} = {ParameterPrefix}{columnMappings[c].ColumnName}";
          command.Parameters.Add(GetParameter(command, columnMappings[c].ColumnName, searchValues[c]));
        }

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="searchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingGreaterThan(ADIFColumnMapping columnMapping, object searchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (searchValue is null)
        throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} > {ParameterPrefix}{columnMapping.ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="searchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingLessThan(ADIFColumnMapping columnMapping, object searchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (searchValue is null)
        throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} < {ParameterPrefix}{columnMapping.ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="searchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingGreaterThanOrEqualTo(ADIFColumnMapping columnMapping, object searchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (searchValue is null)
        throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} >= {ParameterPrefix}{columnMapping.ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="searchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingLessThanOrEqualTo(ADIFColumnMapping columnMapping, object searchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (searchValue is null)
        throw new ArgumentNullException(nameof(searchValue), "Search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} <= {ParameterPrefix}{columnMapping.ColumnName}";
        command.Parameters.Add(GetParameter(command, columnMapping.ColumnName, searchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <param name="startSearchValue"></param>
    /// <param name="endSearchValue"></param>
    public IEnumerable<ADIFQSO> RetrieveByMappingBetween(ADIFColumnMapping columnMapping, object startSearchValue, object endSearchValue)
    {
      if (columnMapping == null || string.IsNullOrEmpty(columnMapping.ColumnName))
        throw new ArgumentException("Invalid column mapping specified.", nameof(columnMapping));

      if (startSearchValue is null)
        throw new ArgumentNullException(nameof(startSearchValue), "Starting search value is required to retrieve data.");

      if (endSearchValue is null)
        throw new ArgumentNullException(nameof(endSearchValue), "Ending search value is required to retrieve data.");

      using (var command = Connection.CreateCommand())
      {
        command.CommandText = $"SELECT * FROM {QSOTable} WHERE {columnMapping.ColumnName} BETWEEN {ParameterPrefix}{columnMapping.ColumnName}1 AND {ParameterPrefix}{columnMapping.ColumnName}2";
        command.Parameters.Add(GetParameter(command, $"{columnMapping.ColumnName}1", startSearchValue));
        command.Parameters.Add(GetParameter(command, $"{columnMapping.ColumnName}2", endSearchValue));

        using (var reader = command.ExecuteReader())
        {
          while (reader != null && reader.Read())
          {
            var qso = GetQSOFromReader(reader);

            if (qso.Count > 0)
              yield return qso;
          }
        }
      }
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="call"></param>
    public IEnumerable<ADIFQSO> RetrieveByCall(string call)
    {
      if (string.IsNullOrEmpty(call))
        throw new ArgumentException("Callsign is required.", nameof(call));

      var callMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Call);

      if (callMapping == null || string.IsNullOrEmpty(callMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Call}");
      
      return RetrieveByMappingEquals(callMapping, call);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="operatorCall"></param>
    public IEnumerable<ADIFQSO> RetrieveByOperator(string operatorCall)
    {
      if (string.IsNullOrEmpty(operatorCall))
        throw new ArgumentException("Operator callsign is required.", nameof(operatorCall));

      var operMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Operator);

      if (operMapping == null || string.IsNullOrEmpty(operMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Operator}");

      return RetrieveByMappingEquals(operMapping, operatorCall);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="qsoStartDate"></param>
    /// <param name="qsoEndDate"></param>
    public IEnumerable<ADIFQSO> RetrieveByQSODateBetween(DateTime qsoStartDate, DateTime qsoEndDate)
    {
      if (qsoStartDate < SQL_MIN_DATE_TIME)
        throw new ArgumentException("Invalid QSO starting date.", nameof(qsoStartDate));

      if (qsoEndDate < SQL_MIN_DATE_TIME)
        throw new ArgumentException("Invalid QSO ending date.", nameof(qsoEndDate));

      if (qsoStartDate > qsoEndDate)
        throw new ArgumentException("QSO starting date cannot be greater than QSO ending date.");

      var dateMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.QSODate);

      if (dateMapping == null || string.IsNullOrEmpty(dateMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.QSODate}");

      return RetrieveByMappingBetween(dateMapping, qsoStartDate.Date, qsoEndDate.Date);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="band"></param>
    /// <returns></returns>
    public IEnumerable<ADIFQSO> RetrieveByBand(string band)
    {
      if (string.IsNullOrEmpty(band))
        throw new ArgumentException("Band is required.", nameof(band));

      if (!Values.Bands.IsValid(band))
        throw new ArgumentException($"Invalid band: {band}", nameof(band));

      var bandMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Band);

      if (bandMapping == null || string.IsNullOrEmpty(bandMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Band}");

      return RetrieveByMappingEquals(bandMapping, band);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    public IEnumerable<ADIFQSO> RetrieveByMode(string mode)
    {
      if (string.IsNullOrEmpty(mode))
        throw new ArgumentException("Mode is required.", nameof(mode));

      if (!Values.Modes.IsValid(mode))
        throw new ArgumentException($"Invalid mode: {mode}", nameof(mode));

      var modeMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Band);

      if (modeMapping == null || string.IsNullOrEmpty(modeMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Mode}");

      return RetrieveByMappingEquals(modeMapping, mode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="band"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public IEnumerable<ADIFQSO> RetrieveByBandMode(string band, string mode)
    {
      if (string.IsNullOrEmpty(band))
        throw new ArgumentException("Band is required.", nameof(band));

      if (!Values.Bands.IsValid(band))
        throw new ArgumentException($"Invalid band: {band}", nameof(band));

      if (string.IsNullOrEmpty(mode))
        throw new ArgumentException("Mode is required.", nameof(mode));

      if (!Values.Modes.IsValid(mode))
        throw new ArgumentException($"Invalid mode: {mode}", nameof(mode));

      var modeMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Band);

      if (modeMapping == null || string.IsNullOrEmpty(modeMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Mode}");

      var bandMapping = ColumnMappings.GetColumnMappingFromTagName(TagNames.Band);

      if (bandMapping == null || string.IsNullOrEmpty(bandMapping.ColumnName))
        throw new Exception($"No mapping found for tag {TagNames.Band}");

      return RetrieveByMappingEquals(new ADIFColumnMapping[] { bandMapping, modeMapping }, new object[] { band, mode });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    ADIFQSO GetQSOFromReader(IDataReader reader)
    {
      var qso = new ADIFQSO();

      if (reader != null)
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

              var userDefDefTag = Header.GetUserDefinedTag(columnMapping.TagName);

              if (userDefDefTag == null)
                throw new Exception($"No user-defined field found with name {columnMapping.TagName}.");

              qso.AddUserDefinedTag(userDefDefTag, reader.GetValue(r));
            }
            else if (columnMapping.IsAppDef)
            {
              qso.Add(new AppDefTag(columnMapping.TagName, reader.GetValue(r)));
            }
            else
            {
              var tag = TagFactory.TagFromName(columnMapping.TagName);

              if (tag == null)
                throw new Exception($"Invalid ADIF tag name: {columnMapping.TagName}");

              if (tag.Header)
                throw new Exception($"Tag {columnMapping.TagName} is a header tag.");

              tag.SetValue(reader.GetValue(r));
              qso.Add(tag);
            }
          }
        }
      }

      return qso;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="mapping"></param>
    object GetParameterValue(ITag tag, ADIFColumnMapping mapping)
    {
      if (tag == null || mapping == null)
        throw new ArgumentNullException("Missing tag or column mapping.");

      if (mapping.IsRequired && !tag.HasValue())
        throw new Exception($"Tag {tag.Name} is marked as required but it has no value.");

      return GetParameterValue(tag.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="value"></param>
    /// <param name="required"></param>
    object GetParameterValue(object value)
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
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="tag"></param>
    /// <param name="mapping"></param>
    /// <param name="direction"></param>
    IDbDataParameter GetParameter(IDbCommand command, 
                                  ITag tag,
                                  ADIFColumnMapping mapping,
                                  ParameterDirection direction = ParameterDirection.Input)
    {

      if (command == null)
        throw new ArgumentNullException(nameof(command), "DB command is required.");

      var param = command.CreateParameter();
      param.ParameterName = $"{ParameterPrefix}{mapping.ColumnName}";
      param.Value = GetParameterValue(tag, mapping);
      param.Direction = direction;

      return param;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <param name="direction"></param>
    IDbDataParameter GetParameter(IDbCommand command,
                                  string name,
                                  object value,
                                  ParameterDirection direction = ParameterDirection.Input)
    {

      if (command == null)
        throw new ArgumentNullException(nameof(command), "DB command is required.");

      var param = command.CreateParameter();
      param.ParameterName = $"{ParameterPrefix}{name}";
      param.Value = GetParameterValue(value);
      param.Direction = direction;

      return param;
    }

    /// <summary>
    /// 
    /// </summary>
    void Connect()
    {
      if (Connection == null)
        throw new ArgumentNullException("SQL connection cannot be null.");

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

        QSOTable = null;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose() => Dispose(true);

    const string UNIQ_ID_APP_DEF_FIELD = TagNames.AppDef + Values.DEFAULT_PROGRAM_ID + "_" + UNIQ_ID_APP_DEF_FIELD_NAME;
    const string UNIQ_ID_APP_DEF_FIELD_NAME = "QSOUNIQID";
    const string UNIQ_ID_SQL_COL = "ADIFNET_UNIQ_ID";
    const string SQL_INSERT_COMMAND_TEXT = "INSERT INTO {0} ({1}) VALUES ({2})";
    const string SQL_SELECT_COMMAND_TEXT = "SELECT * FROM {0}";
    static readonly DateTime SQL_MIN_DATE_TIME = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);
  }

  /// <summary>
  /// 
  /// </summary>
  public class ADIFColumnMappings : List<ADIFColumnMapping> {

    /// <summary>
    /// 
    /// </summary>
    public static readonly ADIFColumnMappings DefaultMinimum = new ADIFColumnMappings(new ADIFColumnMapping(TagNames.Call),
                                                                                      new ADIFColumnMapping(TagNames.Operator),
                                                                                      new ADIFColumnMapping(TagNames.QSODate),
                                                                                      new ADIFColumnMapping(TagNames.TimeOn),
                                                                                      new ADIFColumnMapping(TagNames.Band),
                                                                                      new ADIFColumnMapping(TagNames.Mode));

    /// <summary>
    /// 
    /// </summary>
    public static readonly ADIFColumnMappings All = new ADIFColumnMappings(GetAllMappings().ToArray());



    /// <summary>
    /// 
    /// </summary>
    public ADIFColumnMappings() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mappings"></param>
    public ADIFColumnMappings(params ADIFColumnMapping[] mappings)
    {
      if (mappings != null)
        foreach (var mapping in mappings)
          Add(mapping);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public string GetColumnName(string tagName)
    {
      return GetColumnMappingFromTagName(tagName)?.ColumnName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnName"></param>
    public string GetTagName(string columnName)
    {
      return GetColumnMappingFromColumnName(columnName)?.TagName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public ADIFColumnMapping GetColumnMappingFromTagName(string tagName)
    {
      return this.FirstOrDefault(c => c.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnName"></param>
    public ADIFColumnMapping GetColumnMappingFromColumnName(string columnName)
    {
      return this.FirstOrDefault(c => c.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 
    /// </summary>
    static IEnumerable<ADIFColumnMapping> GetAllMappings()
    {
      foreach (var tagName in TagNames.GetQSOTagNames())
        yield return new ADIFColumnMapping(tagName);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class ADIFColumnMapping {

    public string TagName { get; }
    public string ColumnName { get; }

    public bool IsUserDef { get; }
    public bool IsAppDef { get; }
    public bool IsRequired { get; set; }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="columnName"></param>
    /// <param name="isUserDef"></param>
    /// <param name="isAppDef"></param>
    public ADIFColumnMapping(string tagName, string columnName, bool isUserDef, bool isAppDef)
    {
      if (string.IsNullOrEmpty(tagName))
        throw new ArgumentException("Tag name cannot be null or empty string.", nameof(tagName));

      if (string.IsNullOrEmpty(columnName))
        throw new ArgumentException("Column name cannot be null or empty string.", nameof(columnName));

      if (isUserDef && isAppDef)
        throw new ArgumentException("Mapping cannot represent both app-defined and user-defined field.");

      if (!TagNames.IsTagName(tagName) && !isUserDef && !isAppDef)
        throw new ArgumentException($"Tag {tagName} is not valid.");

      IsUserDef = isUserDef;
      IsAppDef = isAppDef;

      TagName = tagName;
      ColumnName = columnName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public ADIFColumnMapping(string tagName) : this(tagName, tagName, false, false) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="columnName"></param>
    public ADIFColumnMapping(string tagName, string columnName) : this(tagName, columnName, false, false) { }
  }
}
