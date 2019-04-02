using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryBook : BaseRepository<Book>
    {
        public RepositoryBook() : base(@"data source=C:\ProgramData\SEVEN\Database\Database.db;")
        {

        }

        public RepositoryBook(string connectionString) : base(connectionString)
        {
        }

        public Book GetBookByTitle(string title)
        {
            var sql = string.Format("SELECT * FROM Book WHERE Title='{0}'", title);

            return GetUniqueItem(sql, this.MapBook);
        }

        public IEnumerable<Book> GetBooks()
        {
            var sql = "SELECT * FROM Book";

            return (IEnumerable<Book>)GetItems(sql, this.MapBook);
        }

        public bool AddBook(Book book)
        {
            bool hasAuthor = (book.Author != null);

            var sql = String.Format("INSERT INTO Book (Title, Date, Genre, Summary");
            
            if(hasAuthor)
                sql += ", Author";

            sql += String.Format(") VALUES ('{0}', '{1}', {2}, '{3}'", book.Title, book.Date.ToString(), (int)book.Genre, book.Summary);

            if (hasAuthor)
                sql += String.Format(", {0}", book.Author);

            sql += ")";

            return ExecuteNonQuery(sql) == 1;
        }

        public bool DeleteBookByTitle(int title)
        {
            var sql = string.Format("DELETE FROM Book WHERE Title = {0}", title);

            return ExecuteNonQuery(sql) == 1;
        }

        private Book MapBook(SQLiteDataReader reader)
        {
            bool hasAuthor = (reader["Author"].GetType() != typeof(DBNull));

            return new Book()
            {
                Title = reader["Title"].ToString(),
                Date = DateTime.Parse(reader["Date"].ToString()),
                //Date = DateTime.ParseExact(reader["Date"].ToString(), "d", null),
                Genre = (SevenLib.Helpers.Genre)(int)reader["Genre"],
                Summary = reader["Summary"].ToString(),
                Author = hasAuthor ? (Author)reader["Author"] : null,
            };
        }
    }
}
