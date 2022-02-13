﻿using System;
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

    public static readonly SQLiteHelper Instance;

    public bool Exists { get; private set; }

    public bool Connected { get; private set; }

    public bool Ready => Exists && Connected;

    public string Database { get; private set; }

    static SQLiteHelper()
    {
      Instance = new SQLiteHelper();
    }

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

    private SQLiteHelper()
    {
      byte[] bytes = Resources.adif;

      if (bytes != null && bytes.Length > 0)
      {
        var path = Path.GetTempFileName();

        File.WriteAllBytes(path, bytes);
        Exists = true;
        tempPath = path;

        CreateConnection(path, true, false);
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

    public void Dispose() => Dispose(true);

    /// <summary></summary>
    void Dispose(bool disposing)
    {
      Connected = false;
      Exists = false;
      Database = null;

      if (disposing)
      {
        if (connection != null)
          connection.Dispose();

        GC.SuppressFinalize(this);
      }

      DeleteTempFile();
    }

    void DeleteTempFile()
    {
      if (!string.IsNullOrEmpty(tempPath))
      {
        try
        {
          new FileInfo(tempPath).Delete();
          tempPath = null;
        }
        catch
        {
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    public T ExecuteScalar<T>(string query)
    {
      return ExecuteScalar<T>(query, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    public T ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
    {
      var tempResult = ExecuteScalar(query, parameters);

      if (tempResult is T result)
        return result;

      return default(T);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public object ExecuteScalar(string query)
    {
      return ExecuteScalar(query, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public object ExecuteScalar(string query, Dictionary<string, object> parameters)
    {
      CheckConnection();

      using (var command = connection.CreateCommand())
      {
        command.CommandText = query;

        if (parameters != null)
        {
          foreach (var keyValue in parameters)
            command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
        }

        return command.ExecuteScalar();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    public string EscapeString(string str)
    {
      if (str == null)
        return string.Empty;

      return str.Replace("'", "''");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public ArrayList ReadData(string query)
    {
      return ReadData(query, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public ArrayList ReadData(string query, Dictionary<string, object> parameters) {

      CheckConnection();

      var result = new ArrayList();

      using (var command = connection.CreateCommand()) {

        command.CommandText = query;

        if (parameters != null)
        {
          foreach (var keyValue in parameters)
            command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
        }

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

    ~SQLiteHelper() => Dispose(false);

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
    string tempPath;

    }
  }
