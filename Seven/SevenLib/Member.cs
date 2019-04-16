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

        public Member(String lastname, String firstname, String mail, String password, bool isAdmin = false, String tel = "", List<Loan> loans = null, Int64? id = null) : base(lastname, firstname, mail, tel, loans, id)
        {
            this.ID = id;
            this.LastName = lastname;
            this.FirstName = firstname;
            this.Password = password;
            this.Mail = mail;
            this.Tel = tel;
            this.IsAdmin = isAdmin;
            this.Loans = loans;
        }

        #endregion

        #region GETTERS/SETTERS

        public String Password { get; set; }

        public bool IsAdmin { get; set; } = false;

        #endregion
    }
}
