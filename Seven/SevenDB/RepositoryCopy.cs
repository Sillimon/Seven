using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryCopy : BaseRepository<Copy>
    {
        public RepositoryCopy() : base(@"data source=C:\ProgramData\SEVEN\Database\Database.db;")
        {

        }

        public RepositoryCopy(string connectionString) : base(connectionString)
        {
        }

        #region SELECT / GET

        public Copy GetCopyByReference(Int64 reference)
        {
            var sql = "SELECT * FROM Copy WHERE Reference=@Reference";

            return GetUniqueItem(sql, this.MapCopy, command => { command.Parameters.AddWithValue("@Reference", reference); });
        }

        public Int64 GetNumberOfCopies(Int64? id)
        {
            var sql = "SELECT COUNT(*) FROM Copy WHERE Book = @Book";

            return ExecuteScalar(sql, command => { command.Parameters.AddWithValue("@Book", id); });
        }

        public IEnumerable<Copy> GetCopiesByBookTitle(String title)
        {
            //No risks of any SQL injection with this query since it's a public information and the 'LIKE' keyword doesn't allow any inner query.
            //Consequently, no use of the SQLParameters
            var sql = @"SELECT Copy.Reference, Copy.Book
                        FROM Book
                        JOIN Copy ON Copy.Book = Book.Id_Book
                        WHERE Book.Title LIKE ('%' || @title || '%')";

            return GetItems(sql, this.MapCopy,
                cmd => cmd.Parameters.AddWithValue("@title", title));
        }

        public IEnumerable<Copy> GetCopies()
        {
            var sql = "SELECT * FROM Copy INNER JOIN Book ON Copy.Book = Book.Id_Book";

            return GetItems(sql, this.MapCopy);
        }

        #endregion

        #region ADD / INSERT

        public bool AddCopy(Copy copy)
        {
            var sql = "INSERT INTO Copy VALUES (@Reference, @Book)";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Reference", System.Data.DbType.Int64);
                command.Parameters["@Reference"].Value = copy.Reference;

                command.Parameters.Add("@Book", System.Data.DbType.Int64);
                command.Parameters["@Book"].Value = copy.Book.ID;
            }) == 1;
        }

        #endregion

        #region DELETE

        public bool DeleteCopyByID(int id)
        {
            var sql = "DELETE FROM Copy WHERE Reference = @ID";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@ID", System.Data.DbType.Int64);
                command.Parameters["@ID"].Value = id;
            }) == 1;
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditCopy(Copy copyToEdit)
        {
            var sql = "UPDATE Copy " +
                "SET Book = @Book " +
                "WHERE Reference = @Reference";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Book", System.Data.DbType.Int64);
                command.Parameters["@Book"].Value = copyToEdit.Book.ID;

                command.Parameters.Add("@Reference", System.Data.DbType.Int64);
                command.Parameters["@Reference"].Value = copyToEdit.Reference;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Copy MapCopy(DbDataReader reader)
        {
            return new Copy(new RepositoryBook().GetBookByID((Int64)reader["Book"]), (Int64)reader["Reference"]);
        }

        #endregion
    }
}
