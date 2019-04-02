using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public abstract class BaseRepository<T>
    {
        public string ConnectionString { get; set; }

        public BaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected T GetUniqueItem(string sql, Func<SQLiteDataReader, T> mappingMethod)
        {
            var connection = new SQLiteConnection(this.ConnectionString);

            try
            {
                connection.Open();

                var command = new SQLiteCommand(sql, connection);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return mappingMethod(reader);
                }
            }
            finally
            {
                connection.Close();
            }

            return default(T);
        }

        protected IEnumerable<T> GetItems(string sql, Func<SQLiteDataReader, T> mappingMethod)
        {
            var connection = new SQLiteConnection(this.ConnectionString, true);

            try
            {
                connection.Open();

                var command = new SQLiteCommand(sql, connection);

                var reader = command.ExecuteReader();

                var result = new List<T>();

                while (reader.Read())
                {
                    result.Add(mappingMethod(reader));
                }

                return result;
            }
            finally
            {
                connection.Close();
            }
        }

        protected int ExecuteNonQuery(string sql)
        {
            var connection = new SQLiteConnection(this.ConnectionString);

            try
            {
                connection.Open();

                var command = new SQLiteCommand(sql, connection);

                return command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
