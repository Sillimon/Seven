using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryLoan : BaseRepository<Loan>
    {
        public RepositoryLoan() : base(SevenLib.Helpers.Const.DBPath)
        {

        }

        public RepositoryLoan(string connectionString) : base(connectionString)
        {
        }

        #region SELECT / GET

        public Loan GetLoanByID(Int64 id)
        {
            var sql = "SELECT * FROM Loan WHERE Id_Loan=@ID";

            return GetUniqueItem(sql, this.MapLoan, command => { command.Parameters.AddWithValue("@ID", id); });
        }

        public IEnumerable<Loan> GetLoansByMemberID(Int64 id)
        {
            var sql = "SELECT * FROM Loan WHERE Member=@Member";

            return GetItems(sql, this.MapLoan, command => { command.Parameters.AddWithValue("@Member", id); });
        }

        public IEnumerable<Loan> GetLoans()
        {
            var sql = "SELECT * FROM Loan " +
                "INNER JOIN Copy ON Loan.Copy = Copy.Reference " +
                "INNER JOIN Member ON Loan.Member = Member.Id_Member";

            return GetItems(sql, this.MapLoan);
        }

        #endregion

        #region ADD / INSERT

        public bool AddLoan(Loan loan)
        {
            var sql = "INSERT INTO Loan (LoanDate, ReturnDate, Copy, Member) VALUES (@LoanDate, @ReturnDate, @Copy, @Member)";

            String returndate = null;
            if (loan.ReturnDate != null)
                returndate = loan.ReturnDate.Value.ToLongDateString();

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@LoanDate", System.Data.DbType.String);
                command.Parameters["@LoanDate"].Value = loan.LoanDate.ToLongDateString();

                command.Parameters.Add("@ReturnDate", System.Data.DbType.String);
                command.Parameters["@ReturnDate"].Value = returndate;

                command.Parameters.Add("@Copy", System.Data.DbType.Int64);
                command.Parameters["@Copy"].Value = loan.Copy.Reference;

                command.Parameters.Add("@Member", System.Data.DbType.Int64);
                command.Parameters["@Member"].Value = loan.Member.ID;
            }) == 1;
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditLoan(Loan loanToEdit)
        {
            var sql = "UPDATE Loan " +
                "SET ReturnDate = @ReturnDate " +
                "WHERE Id_Loan = @ID";

            String returndate = null;
            if (loanToEdit.ReturnDate != null)
                returndate = loanToEdit.ReturnDate.Value.ToLongDateString();

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@ReturnDate", System.Data.DbType.String);
                command.Parameters["@ReturnDate"].Value = returndate;

                command.Parameters.Add("@ID", System.Data.DbType.Int64);
                command.Parameters["@ID"].Value = loanToEdit.ID;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Loan MapLoan(DbDataReader reader)
        {
            DateTime? returndate = null;
            if (reader["ReturnDate"] != DBNull.Value)
            {
                returndate = DateTime.Parse((String)reader["ReturnDate"]);
            }

            return new Loan()
            {
                ID = (Int64)reader["Id_Loan"],
                LoanDate = DateTime.Parse((string)reader["LoanDate"]),
                ReturnDate = returndate,
                Copy = new RepositoryCopy(SevenLib.Helpers.Const.DBPath).GetCopyByReference((Int64)reader["Copy"]),
                Member = new RepositoryMember(SevenLib.Helpers.Const.DBPath).GetMemberByID((Int64)reader["Member"])
            };
        }

        #endregion
    }
}
