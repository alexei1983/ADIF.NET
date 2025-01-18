using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;

namespace org.goodspace.Data.Radio.Adif.Helpers
{
    /// <summary>
    /// Provides SQLite database helper methods.
    /// </summary>
    internal class SQLiteHelper : IDisposable
    {
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
        /// <param name="commandText"></param>
        public int ExecuteNonQuery(string commandText)
        {
            CheckConnection();

            if (memoryConnection == null)
                return 0;

            using var command = memoryConnection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Drops the specified table.
        /// </summary>
        /// <param name="table">Name of the table that will be dropped.</param>
        public int DropTable(string table)
        {
            CheckConnection();

            if (memoryConnection == null)
                return 0;

            using var command = memoryConnection.CreateCommand();
            command.CommandText = $"DROP TABLE \"{table}\"";
            return command.ExecuteNonQuery();
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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing)
        {
            if (disposing)
            {
                memoryConnection?.Dispose();
                Connected = false;
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
                    tempPath = null;
                    if (fi.Exists)
                        fi.Delete();
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
        public T? ExecuteScalar<T>(string query)
        {
            return ExecuteScalar<T>(query, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        public T? ExecuteScalar<T>(string query, Dictionary<string, object?>? parameters)
        {
            var tempResult = ExecuteScalar(query, parameters);

            if (tempResult is T result)
                return result;

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        public object? ExecuteScalar(string query)
        {
            return ExecuteScalar(query, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        public object? ExecuteScalar(string query, Dictionary<string, object?>? parameters)
        {
            CheckConnection();

            if (memoryConnection == null)
                return default;

            using var command = memoryConnection.CreateCommand();
            command.CommandText = query;

            if (parameters != null)
            {
                foreach (var keyValue in parameters)
                    command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
            }

            return command.ExecuteScalar();
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
        /// <param name="parameters"></param>
        public ArrayList ReadData(string query, Dictionary<string, object?>? parameters)
        {
            CheckConnection();

            if (memoryConnection == null)
                return [];

            var result = new ArrayList();

            using (var command = memoryConnection.CreateCommand())
            {
                command.CommandText = query;

                if (parameters != null)
                {
                    foreach (var keyValue in parameters)
                        command.Parameters.AddWithValue(keyValue.Key, keyValue.Value);
                }

                using var dataReader = command.ExecuteReader();
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

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        ~SQLiteHelper() => Dispose(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expando"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        static ExpandoObject? AddProperty(ExpandoObject expando,
                                          string propertyName,
                                          object? propertyValue)
        {

            if (expando is IDictionary<string, object?> expandoDict)
            {
                if (expandoDict.ContainsKey(propertyName))
                    expandoDict[propertyName] = propertyValue;
                else
                    expandoDict.Add(propertyName, propertyValue);

                return (ExpandoObject)expandoDict;
            }
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        void CheckConnection()
        {
            if (memoryConnection == null)
                throw new InvalidOperationException("ADIF database connection has not been initialized.");

            if (memoryConnection.State == ConnectionState.Closed || memoryConnection.State == ConnectionState.Broken)
                throw new InvalidOperationException("ADIF database connection is invalid.");
        }

        SQLiteConnection? memoryConnection;
        string? tempPath;
    }
}
