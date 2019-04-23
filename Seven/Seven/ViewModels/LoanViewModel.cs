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

            RefreshCopies();
            RefreshLoans();
        }

        #endregion

        #region METHODS

        public void RefreshCopies()
        {
            RefreshRepositories();
            copies = rc.GetCopiesNotBorrowed().ToList<Copy>();
        }

        public List<Copy> SearchCopy(String title)
        {
            RefreshRepositories();
            return rc.GetCopiesByBookTitle(title).ToList<Copy>();
        }

        public void RefreshLoans()
        {
            if (MainWindow.currentMember.ID == null)
                return;

            RefreshRepositories();

            loans = rl.GetLoansByMemberID((Int64)MainWindow.currentMember.ID).ToList<Loan>();
        }

        private void RefreshRepositories()
        {
            rc.ConnectionString = SevenLib.Helpers.Const.DBPath;
            rl.ConnectionString = SevenLib.Helpers.Const.DBPath;
        }

        public void BorrowCopy(Copy copy)
        {
            copy.Borrowed = true;
            EditCopy(copy);
        }

        public void ReturnCopy(Loan loan)
        {
            loan.Copy.Borrowed = false;
            EditLoan(loan);
            EditCopy(loan.Copy);
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddLoan(Loan loan)
        {
            RefreshRepositories();
            return rl.AddLoan(loan);
        }

        public bool EditLoan(Loan loan)
        {
            RefreshRepositories();
            return rl.EditLoan(loan);
        }

        public bool EditCopy(Copy copyToEdit)
        {
            RefreshRepositories();
            return rc.EditCopy(copyToEdit);
        }

        #endregion
    }
}
