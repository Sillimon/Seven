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
    /// Logique d'interaction pour ucAuthor.xaml
    /// </summary>
    public partial class ucAuthor : UserControl
    {
        AuthorViewModel authorModel;

        public ucAuthor()
        {
            InitializeComponent();

            authorModel = new AuthorViewModel();

            dgvAuthors.ItemsSource = authorModel.authors;
            refreshAuthors();
        }

        #region METHODS

        public void refreshAuthors()
        {
            authorModel.refreshAuthors();
            dgvAuthors.ItemsSource = authorModel.authors;
            dgvAuthors.Items.Refresh();
        }

        #endregion


        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DataContext = authorModel;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            dgvAuthors.ItemsSource = authorModel.searchAuthor(tbSearch.Text);
        }
        
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditAuthor addWindow = new AddEditAuthor();

            addWindow.ShowDialog();

            if(addWindow.DialogResult == true)
            {
                if (authorModel.AddAuthor(addWindow.GetSelectedAuthor()))
                {
                    MessageBox.Show("Author was added successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    refreshAuthors();
                }
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var a in dgvAuthors.SelectedItems)
                {
                    Author author = a as Author;

                    authorModel.DeleteAuthor(author);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Message : {0}" +
                    "\nStackTrace : {1}",
                    ex.Message,
                    ex.StackTrace),
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                refreshAuthors();
            }
        }
        
        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgvAuthors.SelectedItem == null)
            {
                MessageBox.Show("Select an author to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddEditAuthor editWindow = new AddEditAuthor((Author)dgvAuthors.SelectedItem);

            editWindow.ShowDialog();

            if (editWindow.DialogResult == true)
            {
                if (authorModel.EditAuthor(editWindow.GetSelectedAuthor()))
                {
                    MessageBox.Show("Author was modified successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    refreshAuthors();
                }
            }
        }


        private void DgvAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvAuthors.SelectedItem == null)
                return;

            Author selectedAuthor = (Author)dgvAuthors.SelectedItem;

            this.SelectedAuthorName.Text = selectedAuthor.ToString();
            this.SelectedAuthorBirthDate.Text = selectedAuthor.BirthDate.ToLongDateString();

            if(selectedAuthor.DeathDate != null)
                this.SelectedAuthorDeathDate.Text = DateTime.Parse(selectedAuthor.DeathDate.ToString()).ToLongDateString();

            this.SelectedAuthorNationality.Text = SevenLib.Helpers.EnumHelpers.ToDescriptionString(selectedAuthor.Nationality);

            this.SelectedAuthorBooks.Text = "TODO";
        }
    }
}
