using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SevenDB
{
    public abstract class BaseRepository<T>
    where T : class
    {
        public string ConnectionString { get; set; }

        public BaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected T GetUniqueItem(string sql, Func<SQLiteDataReader, T> mappingMethod, Action<SQLiteCommand> action = null)
        {
            using (var connection = new SQLiteConnection(this.ConnectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    action?.Invoke(command);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return mappingMethod(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        protected IEnumerable<T> GetItems(string sql, Func<SQLiteDataReader, T> mappingMethod, Action<SQLiteCommand> action = null)
        {
            using (var connection = new SQLiteConnection(this.ConnectionString, true))
            {

                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {

                    var result = new List<T>();
                    
                    action?.Invoke(command);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(mappingMethod(reader));
                        }
                    }

                    return result;
                }
            }
        }

        protected int ExecuteNonQuery(string sql, Action<SQLiteCommand> action = null)
        {
            using (var connection = new SQLiteConnection(this.ConnectionString))
            {

                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    action?.Invoke(command);
                    return command.ExecuteNonQuery();
                }
            }
        }

        protected Int64 ExecuteScalar(string sql, Action<SQLiteCommand> action = null)
        {
            using (var connection = new SQLiteConnection(this.ConnectionString))
            {

                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    action?.Invoke(command);
                    return (Int64)command.ExecuteScalar();
                }
            }
        }
    }
}
