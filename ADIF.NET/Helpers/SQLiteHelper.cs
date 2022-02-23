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

    /// <summary>
    /// 
    /// </summary>
    public static readonly SQLiteHelper Instance;

    /// <summary>
    /// 
    /// </summary>
    public bool Connected { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    static SQLiteHelper()
    {
      Instance = new SQLiteHelper();
    }

    /// <summary>
    /// 
    /// </summary>
    private SQLiteHelper()
    {
      byte[] bytes = Resources.adif;

      if (bytes != null && bytes.Length > 0)
      {
        var path = Path.GetTempFileName();

        File.WriteAllBytes(path, bytes);
        tempPath = path;

        CopyDatabaseToMemory();
      }
    }

    /// <summary>
    /// 
    /// </summary>
    void CopyDatabaseToMemory()
    {
      try
      {
        using (var fileConnection = new SQLiteConnection($"Data Source={tempPath};Version=3;Compress=True;"))
        {
          fileConnection.Open();
          memoryConnection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;Compress=True");
          memoryConnection.Open();
          Connected = true;
          fileConnection.BackupDatabase(memoryConnection, "main", "main", -1, null, 0);
        }

        DeleteTempFile();
      }
      catch (Exception ex)
      {
        Connected = false;
        throw new InvalidOperationException($"Could not initialize ADIF database: {ex.Message}", ex);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    public int ExecuteNonQuery(string commandText)
    {
      CheckConnection();

      using (var command = memoryConnection.CreateCommand())
      {
        command.CommandText = commandText;
        return command.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// Drops the specified table.
    /// </summary>
    /// <param name="table">Name of the table that will be dropped.</param>
    public int DropTable(string table)
    {
      CheckConnection();

      using (var command = memoryConnection.CreateCommand())
      {
        command.CommandText = $"DROP TABLE \"{table}\"";
        return command.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// Closes the database connection.
    /// </summary>
    public void CloseConnection()
    {
      if (memoryConnection != null)
      {
        memoryConnection.Close();
        Connected = false;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose() => Dispose(true);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (memoryConnection != null)
          memoryConnection.Dispose();

        Connected = false;

        GC.SuppressFinalize(this);
      }

      DeleteTempFile();
    }

    /// <summary>
    /// 
    /// </summary>
    void DeleteTempFile()
    {
      if (!string.IsNullOrEmpty(tempPath))
      {
        try
        {
          var fi = new FileInfo(tempPath);
          if (fi.Exists)
            fi.Delete();
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

      using (var command = memoryConnection.CreateCommand())
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
    public ArrayList ReadData(string query, Dictionary<string, object> parameters)
    {
      CheckConnection();

      var result = new ArrayList();

      using (var command = memoryConnection.CreateCommand())
      {
        command.CommandText = query;

        if (parameters != null)
        {
          foreach (var keyValue in parameters)
            command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
        }

        using (var dataReader = command.ExecuteReader())
        {
          while (dataReader.Read())
          {
            // get the number of values
            var valCnt = dataReader.VisibleFieldCount;
            dynamic dynObj = new ExpandoObject();

            for (var x = 0; x < valCnt; x++)
            {
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
    ExpandoObject AddProperty(ExpandoObject expando,
                              string propertyName,
                              object propertyValue)
    {

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
    void CheckConnection()
    {
      if (memoryConnection == null)
        throw new InvalidOperationException("ADIF database connection has not been initialized.");
    }

    SQLiteConnection memoryConnection;
    string tempPath;

  }
}
