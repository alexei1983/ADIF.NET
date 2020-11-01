using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;


namespace ADIF.NET.Helpers {

  /// <summary>
  /// Provides SQLite database helper methods.
  /// </summary>
  public class SQLiteHelper : IDisposable {

    public bool Exists { get; private set; }

    public bool Connected { get; private set; }

    public bool Ready => Exists && Connected;

    public string Database { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    public SQLiteHelper(string filename) : this(filename, true, false) {
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="compress"></param>
    /// <param name="isNew"></param>
    public SQLiteHelper(string filename, bool compress, bool isNew = false) {

      Exists = new FileInfo(filename)?.Exists ?? false;

      if (Exists || isNew) {
        CreateConnection(filename, compress, isNew);
        }
      }

    public SQLiteHelper(byte[] database, bool compress = true) {

      if (database != null && database.Length > 0) {
        var path = Path.GetTempFileName();

        File.WriteAllBytes(path, database);
        Exists = true;

        CreateConnection(path, compress, false);
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="compress"></param>
    /// <param name="isNew"></param>
    /// <returns></returns>
    SQLiteConnection CreateConnection(string filename, 
                                      bool compress = true,
                                      bool isNew = false) {

      var connString = $@"Data Source={filename};Version = 3;{(isNew ? "New = True;" : string.Empty)}{(compress ? "Compress = True;" : string.Empty)}";

      // create a new database connection:
      connection = new SQLiteConnection(connString);

      // open the connection:
      try {
        connection.Open();
        Connected = true;

        Database = filename;

        if (isNew)
          Exists = true;
        }
      catch (Exception ex) {
        Connected = false;
        Exists = false;
        throw new InvalidOperationException(ex.Message, ex);
        }
      return connection;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(string commandText) {

      CheckConnection();

      using (var command = connection.CreateCommand()) {
        command.CommandText = commandText;
        return command.ExecuteNonQuery();
        }
      }

    /// <summary>
    /// Drops the specified table.
    /// </summary>
    /// <param name="table">Name of the table that will be dropped.</param>
    public int DropTable(string table) {

      CheckConnection();

      using (var command = connection.CreateCommand()) {
        command.CommandText = $"DROP TABLE \"{table}\"";
        return command.ExecuteNonQuery();
        }
      }

    /// <summary>
    /// Closes the database connection.
    /// </summary>
    public void CloseConnection() {
      if (connection != null) {
        connection.Close();
        Connected = false;
        }
      }

    /// <summary></summary>
    public void Dispose() {

      if (connection != null)
        connection.Dispose();

      Connected = false;
      Exists = false;
      Database = null;

      GC.SuppressFinalize(this);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public ArrayList ReadData(string query) {

      CheckConnection();

      var result = new ArrayList();

      using (var command = connection.CreateCommand()) {

        command.CommandText = query;

        using (var dataReader = command.ExecuteReader()) {

          while (dataReader.Read()) {

            // get the number of values
            var valCnt = dataReader.VisibleFieldCount;
            dynamic dynObj = new ExpandoObject();

            for (var x = 0; x < valCnt; x++) {
              var colName = dataReader.GetName(x);
              var val = dataReader.GetValue(x);

              dynObj = AddProperty(dynObj, colName, val);
              }
            result.Add(dynObj);
            }
          }
        }

      return result;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expando"></param>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    ExpandoObject AddProperty(ExpandoObject expando,
                              string propertyName,
                              object propertyValue) {

      var expandoDict = expando as IDictionary<string, object>;

      if (expandoDict.ContainsKey(propertyName))
        expandoDict[propertyName] = propertyValue;
      else
        expandoDict.Add(propertyName, propertyValue);

      return expandoDict as ExpandoObject;
      }

    /// <summary>
    /// 
    /// </summary>
    void CheckConnection() {
      if (connection == null) 
        throw new InvalidOperationException("Connection has not been initialized.");
      }

    SQLiteConnection connection;

    }
  }
