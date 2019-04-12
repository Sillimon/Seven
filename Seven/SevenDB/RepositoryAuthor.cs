using SevenLib;
using SevenLib.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryAuthor : BaseRepository<Author>
    {
        public RepositoryAuthor() : base(@"data source=C:\ProgramData\SEVEN\Database\Database.db;")
        {

        }

        public RepositoryAuthor(string connectionString) : base(connectionString)
        {
        }

        #region SELECT / GET

        public Author GetAuthorById(Int64 id)
        {
            var sql = String.Format("SELECT * FROM Author WHERE Id_Author = @ID");

            return GetUniqueItem(sql, this.MapAuthor, command => { command.Parameters.AddWithValue("@ID", id); });
        }

        public Author GetAuthorByLastName(string lastname)
        {
            var sql = string.Format("SELECT * FROM Author WHERE LastName = @LastName");

            return GetUniqueItem(sql, this.MapAuthor, command => { command.Parameters.AddWithValue("@LastName", lastname); });
        }

        public IEnumerable<Author> GetAuthors()
        {
            var sql = "SELECT * FROM Author";

            return (IEnumerable<Author>)GetItems(sql, this.MapAuthor);
        }

        public IEnumerable<Author> GetAuthorsByBeginningOfLastName(String lastname)
        {
            //No risks of any SQL injection with this query since it's a public information and the 'LIKE' keyword doesn't allow any inner query.
            //Consequently, no use of the SQLParameters
            var sql = String.Format("SELECT * FROM Author WHERE LastName LIKE '{0}%'", lastname);

            return GetItems(sql, this.MapAuthor);
        }

        #endregion

        #region ADD / INSERT

        public bool AddAuthor(Author author)
        {
            var sql = "INSERT INTO Author (LastName, FirstName, BirthDate, DeathDate, Nationality) VALUES (@LastName, @FirstName, @BirthDate, @DeathDate, @Nationality)";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@LastName", System.Data.DbType.String);
                command.Parameters["@LastName"].Value = author.LastName;

                command.Parameters.Add("@FirstName", System.Data.DbType.String);
                command.Parameters["@FirstName"].Value = author.FirstName;

                command.Parameters.Add("@BirthDate", System.Data.DbType.String);
                command.Parameters["@BirthDate"].Value = author.BirthDate.ToLongDateString();

                command.Parameters.Add("@DeathDate", System.Data.DbType.String);
                command.Parameters["@DeathDate"].Value = author.DeathDate.Value.ToLongDateString();

                command.Parameters.Add("@Nationality", System.Data.DbType.UInt16);
                command.Parameters["@Nationality"].Value = (int)author.Nationality;
            }) == 1;
        }

        #endregion

        #region DELETE

        public bool DeleteAuthorByID(int id)
        {
            var sql = "DELETE FROM Author WHERE Id_Author = @id";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@id", System.Data.DbType.Int64);
                command.Parameters["@id"].Value = id;
            }) == 1;
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditAuthor(Author authorToEdit)
        {
            var sql = "UPDATE Author " +
                "SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate";

            if (authorToEdit.DeathDate != null)
                sql += ", DeathDate = @DeathDate";

            sql += ", Nationality = @Nationality " +
                "WHERE Id_Author = @ID";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@LastName", System.Data.DbType.String);
                command.Parameters["@LastName"].Value = authorToEdit.LastName;

                command.Parameters.Add("@FirstName", System.Data.DbType.String);
                command.Parameters["@FirstName"].Value = authorToEdit.FirstName;

                command.Parameters.Add("@BirthDate", System.Data.DbType.String);
                command.Parameters["@BirthDate"].Value = authorToEdit.BirthDate.ToLongDateString();

                if(authorToEdit.DeathDate != null)
                {
                    command.Parameters.Add("@DeathDate", System.Data.DbType.String);
                    command.Parameters["@DeathDate"].Value = DateTime.Parse(authorToEdit.DeathDate.ToString()).ToLongDateString();
                }

                command.Parameters.Add("@Nationality", System.Data.DbType.Int64);
                command.Parameters["@Nationality"].Value = (Int64)authorToEdit.Nationality;

                command.Parameters.Add("@ID", System.Data.DbType.Int64);
                command.Parameters["@ID"].Value = authorToEdit.ID;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Author MapAuthor(DbDataReader reader)
        {
            Int64? id = (Int64?)reader["Id_Author"];
            String lastname = (String)reader["LastName"];
            String firstname = (string)reader["FirstName"];
            DateTime birthdate = DateTime.Parse((string)reader["BirthDate"]);

            DateTime? deathdate = null;
            if (reader["DeathDate"] != DBNull.Value)
            {
                deathdate = DateTime.Parse((String)reader["DeathDate"]);
            }

            Nationality nationality = (Nationality)(Int64)reader["Nationality"];

            return new Author(lastname, firstname, birthdate, nationality, deathdate, id);
        }

        #endregion
    }
}
