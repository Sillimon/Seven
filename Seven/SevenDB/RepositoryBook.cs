using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        #region SELECT / GET

        public Book GetBookByTitle(string title)
        {
            var sql = "SELECT * FROM Book WHERE Title=@Title";

            return GetUniqueItem(sql, this.MapBook, command => { command.Parameters.AddWithValue("@Title", title); });
        }

        public IEnumerable<Book> GetBooksByBeginningOfTitle(String title)
        {
            //No risks of any SQL injection with this query since it's a public information and the 'LIKE' keyword doesn't allow any inner query.
            //Consequently, no use of the SQLParameters
            var sql = String.Format("SELECT * FROM Book WHERE Title LIKE '{0}%'", title);

            return GetItems(sql, this.MapBook);
        }

        public IEnumerable<Book> GetBooks()
        {
            var sql = "SELECT * FROM Book INNER JOIN Author ON Book.Author = Author.Id_Author";

            return GetItems(sql, this.MapBook);
        }

        #endregion

        #region ADD / INSERT

        public bool AddBook(Book book)
        {
            var sql = "INSERT INTO Book (Title, Date, Genre, Summary, Author) VALUES (@Title, @Date, @Genre, @Summary, @Author)";
            
            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Title", System.Data.DbType.String);
                command.Parameters["@Title"].Value = book.Title;

                command.Parameters.Add("@Date", System.Data.DbType.String);
                command.Parameters["@Date"].Value = book.Date.ToLongDateString();

                command.Parameters.Add("@Genre", System.Data.DbType.Int64);
                command.Parameters["@Genre"].Value = (Int64)book.Genre;

                command.Parameters.Add("@Summary", System.Data.DbType.String);
                command.Parameters["@Summary"].Value = book.Summary;

                command.Parameters.Add("@Author", System.Data.DbType.Int64);
                command.Parameters["@Author"].Value = book.Author.ID;
            }) == 1;
        }

        #endregion

        #region DELETE

        public bool DeleteBookByID(int id)
        {
            var sql = "DELETE FROM Book WHERE Id_Book = @id";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@id", System.Data.DbType.Int64);
                command.Parameters["@id"].Value = id;
            }) == 1;
        }


        public bool DeleteBookByTitle(String title)
        {
            var sql = "DELETE FROM Book WHERE Title = '@Title'";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Title", System.Data.DbType.String);
                command.Parameters["@Title"].Value = title;
            }) == 1;
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditBook(Book bookToEdit)
        {
            var sql = "UPDATE Book " +
                "SET Title = @Title, Date = @Date, Genre = @Genre, Summary = @Summary, Author = @Author " +
                "WHERE Id_Book = @ID";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Title", System.Data.DbType.String);
                command.Parameters["@Title"].Value = bookToEdit.Title;

                command.Parameters.Add("@Date", System.Data.DbType.String);
                command.Parameters["@Date"].Value = bookToEdit.Date.ToLongDateString();

                command.Parameters.Add("@Genre", System.Data.DbType.Int64);
                command.Parameters["@Genre"].Value = (Int64)bookToEdit.Genre;

                command.Parameters.Add("@Summary", System.Data.DbType.String);
                command.Parameters["@Summary"].Value = bookToEdit.Summary;

                command.Parameters.Add("@Author", System.Data.DbType.Int64);
                command.Parameters["@Author"].Value = bookToEdit.Author.ID;

                command.Parameters.Add("@ID", System.Data.DbType.Int64);
                command.Parameters["@ID"].Value = bookToEdit.ID;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Book MapBook(DbDataReader reader)
        {
            return new Book()
            {
                ID = (Int64)reader["Id_Book"],
                Title = (string)reader["Title"],
                Date = DateTime.Parse((string)reader["Date"]),
                Genre = (SevenLib.Helpers.Genre)(Int64)reader["Genre"],
                Summary = (string)reader["Summary"],
                Author = new RepositoryAuthor().GetAuthorById((Int64)reader["Author"])
            };
        }

        #endregion
    }
}
