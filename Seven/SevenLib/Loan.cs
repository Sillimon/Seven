using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Loan
    {
        #region CONSTRUCTORS

        public Loan() { }

        public Loan(DateTime loandate, Copy copy, Member member, DateTime? returndate = null, Int64? id = null)
        {
            this.ID = id;
            this.LoanDate = loandate;
            this.ReturnDate = returndate;
            this.Copy = copy;
            this.Member = member;
        }

        #endregion

        #region GETTERS/SETTERS

        public Int64? ID { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public Copy Copy { get; set; }

        public Member Member { get; set; }

        #endregion
    }
}
