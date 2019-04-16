using Seven.ViewModels;
using SevenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seven
{
    /// <summary>
    /// Logique d'interaction pour ucLoan.xaml
    /// </summary>
    public partial class ucLoan : UserControl
    {
        LoanViewModel loanModel;

        public ucLoan()
        {
            InitializeComponent();

            loanModel = new LoanViewModel();

            dgvCopy.ItemsSource = loanModel.copies;
            dgvLoan.ItemsSource = loanModel.loans;

            refreshCopies();
            refreshLoans();
        }

        #region METHODS

        public void refreshCopies()
        {
            loanModel.refreshCopies();
            dgvCopy.ItemsSource = loanModel.copies;
            dgvCopy.Items.Refresh();
        }

        public void refreshLoans()
        {
            loanModel.refreshLoans();
            dgvLoan.ItemsSource = loanModel.loans;
            dgvLoan.Items.Refresh();
        }

        #endregion

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            dgvCopy.ItemsSource = loanModel.searchCopy(TbSearchCopy.Text);
        }

        private void BtBorrow_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCopy.SelectedItem == null)
                return;

            Copy copy = (Copy)dgvCopy.SelectedItem;

            MessageBoxResult mbr = MessageBox.Show("Do you really want to Borrow " + copy.Book.Title, "Confirm ?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (mbr == MessageBoxResult.Yes)
            {
                loanModel.AddLoan();
            }
            

            
        }
    }
}
