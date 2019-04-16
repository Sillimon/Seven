using SevenDB;
using SevenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven.ViewModels
{
    class LoanViewModel
    {
        public RepositoryCopy rc;
        public RepositoryLoan rl;

        public List<Copy> copies;
        public List<Loan> loans;

        #region CONSTRUCTORS

        public LoanViewModel()
        {
            rc = new RepositoryCopy();
            rl = new RepositoryLoan();

            refreshCopies();
            refreshLoans();
        }

        #endregion

        #region METHODS

        public void refreshCopies()
        {
            copies = rc.GetCopies().ToList<Copy>();
        }

        public List<Copy> searchCopy(String title)
        {
            return rc.GetCopiesByBookTitle(title).ToList<Copy>();
        }

        public void refreshLoans()
        {
            //Get user and search loans for this user

            //loans = rl.GetCopies().ToList<Copy>();
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddLoan(Loan loan)
        {
            return rl.AddLoan(loan);
        }

        public bool EditLoan(Loan loan)
        {
            return rl.EditLoan(loan);
        }

        #endregion
    }
}
