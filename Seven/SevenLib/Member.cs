using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Member : User
    {
        #region CONSTRUCTORS

        public Member(String lastname, String firstname, String mail, String password, bool isAdmin = false, String tel = "", Int64? id = null) : base(lastname, firstname, mail, tel, id)
        {
            this.ID = id;
            this.LastName = lastname;
            this.FirstName = firstname;
            this.Password = password;
            this.Mail = mail;
            this.Tel = tel;
            this.IsAdmin = isAdmin;
        }

        #endregion

        #region GETTERS/SETTERS

        private String m_password;
        public String Password
        {
            get { return this.m_password; }
            set { m_password = Helpers.HashHelper.HashString(value); }
        }

        public bool IsAdmin { get; set; } = false;

        #endregion
    }
}
