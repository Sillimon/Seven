using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryMember : BaseRepository<Member>
    {
        public RepositoryMember() : base(SevenLib.Helpers.Const.DBPath)
        {

        }

        public RepositoryMember(string connectionString) : base(connectionString)
        {
        }

        #region SELECT / GET

        public Member GetMemberByID(Int64 id)
        {
            var sql = "SELECT * FROM Member WHERE Id_Member=@ID";

            return GetUniqueItem(sql, this.MapMember, command => { command.Parameters.AddWithValue("@ID", id); });
        }

        public IEnumerable<Member> GetMembers()
        {
            var sql = "SELECT * FROM Member";

            return GetItems(sql, this.MapMember);
        }

        public Member GetMemberByIdentifiers(String login, String password)
        {
            var sql = "SELECT * FROM Member " +
                "WHERE Mail = @Mail " +
                "AND Password = @Password";

            return GetUniqueItem(sql, this.MapMember, command => 
            {
                command.Parameters.AddWithValue("@Mail", login);
                command.Parameters.AddWithValue("@Password", password);
            });
        }

        #endregion

        #region ADD / INSERT

        public bool AddMember(Member member)
        {
            var sql = "INSERT INTO Member (FirstName, LastName, Password, Mail, Tel, IsAdmin) " +
                "VALUES (@FirstName, @LastName, @Password, @Mail, @Tel, @IsAdmin)";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@FirstName", System.Data.DbType.String);
                command.Parameters["@FirstName"].Value = member.FirstName;

                command.Parameters.Add("@LastName", System.Data.DbType.String);
                command.Parameters["@LastName"].Value = member.LastName;

                command.Parameters.Add("@Password", System.Data.DbType.String);
                command.Parameters["@Password"].Value = member.Password;

                command.Parameters.Add("@Mail", System.Data.DbType.String);
                command.Parameters["@Mail"].Value = member.Mail;

                command.Parameters.Add("@Tel", System.Data.DbType.String);
                command.Parameters["@Tel"].Value = member.Tel;

                command.Parameters.Add("@IsAdmin", System.Data.DbType.Boolean);
                command.Parameters["@IsAdmin"].Value = member.IsAdmin;
            }) == 1;
        }

        #endregion

        #region DELETE

        public bool DeleteMemberByID(int id)
        {
            var sql = "DELETE FROM Member WHERE Id_Member = @id";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@id", System.Data.DbType.Int64);
                command.Parameters["@id"].Value = id;
            }) == 1;
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditMember(Member memberToEdit)
        {
            var sql = "UPDATE Member " +
                "SET Firstname = @FirstName, LastName = @LastName, Password = @Password, Mail = @Mail, Tel = @Tel, IsAdmin = @IsAdmin " +
                "WHERE Id_Member = @ID";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@FirstName", System.Data.DbType.String);
                command.Parameters["@FirstName"].Value = memberToEdit.FirstName;

                command.Parameters.Add("@LastName", System.Data.DbType.String);
                command.Parameters["@LastName"].Value = memberToEdit.LastName;

                command.Parameters.Add("@Password", System.Data.DbType.String);
                command.Parameters["@Password"].Value = memberToEdit.Password;

                command.Parameters.Add("@Mail", System.Data.DbType.String);
                command.Parameters["@Mail"].Value = memberToEdit.Mail;

                command.Parameters.Add("@Tel", System.Data.DbType.String);
                command.Parameters["@Tel"].Value = memberToEdit.Tel;

                command.Parameters.Add("@IsAdmin", System.Data.DbType.Boolean);
                command.Parameters["@IsAdmin"].Value = memberToEdit.IsAdmin;

                command.Parameters.Add("@ID", System.Data.DbType.Int64);
                command.Parameters["@ID"].Value = memberToEdit.ID;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Member MapMember(DbDataReader reader)
        {
            Int64 id = (Int64)reader["Id_Member"];
            String lastname = (String)reader["LastName"];
            String firstname = (String)reader["FirstName"];
            String mail = (String)reader["Mail"];
            String password = (String)reader["Password"];
            bool isAdmin = (Int64)reader["IsAdmin"] != 0;
            String tel = (String)reader["Tel"];


            return new Member(lastname, firstname, mail, password, isAdmin, tel, id);
        }

        #endregion
    }
}
