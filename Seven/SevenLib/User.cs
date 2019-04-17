using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class User
    {
        #region CONSTRUCTORS

        public User(String lastname, String firstname, String mail, String tel = "", Int64? id = null)
        {
            this.ID = id;
            this.LastName = lastname;
            this.FirstName = firstname;
            this.Mail = mail;
            this.Tel = tel;
        }

        #endregion

        #region GETTERS/SETTERS

        public Int64? ID { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public String Mail { get; set; }

        public String Tel { get; set; }

        #endregion

        #region METHODS

        public override String ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

        #endregion
    }
}
