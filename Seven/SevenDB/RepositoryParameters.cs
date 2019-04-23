using SevenLib;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDB
{
    public class RepositoryParameters : BaseRepository<Param>
    {
        public RepositoryParameters() : base(SevenLib.Helpers.Const.DBPath)
        {

        }

        public RepositoryParameters(string connectionString) : base(connectionString)
        {
        }

        #region SELECT / GET

        public Param GetParamByName(String name)
        {
            var sql = String.Format("SELECT * FROM Parameters WHERE Param_Name = @Name");

            return GetUniqueItem(sql, this.MapParam, command => { command.Parameters.AddWithValue("@Name", name); });
        }

        #endregion

        #region EDIT / UPDATE

        public bool EditParam(Param paramToEdit)
        {
            var sql = "UPDATE Parameters " +
                "SET Param_Value = @Value " +
                "WHERE Param_Name = @Name";

            return ExecuteNonQuery(sql, command =>
            {
                command.Parameters.Add("@Value", System.Data.DbType.String);
                command.Parameters["@Value"].Value = paramToEdit.Value;

                command.Parameters.Add("@Name", System.Data.DbType.String);
                command.Parameters["@Name"].Value = paramToEdit.Name;
            }) == 1;
        }

        #endregion

        #region HELPERS

        private Param MapParam(DbDataReader reader)
        {
            return new Param()
            {
                Name = (String)reader["Param_Name"],
                Value = (String)reader["Param_Value"]
            };
        }

        #endregion
    }
}
