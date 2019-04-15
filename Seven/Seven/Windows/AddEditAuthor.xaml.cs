using SevenDB;
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
using System.Windows.Shapes;

namespace Seven
{
    /// <summary>
    /// Logique d'interaction pour AddEditAuthor.xaml
    /// </summary>
    public partial class AddEditAuthor : Window
    {
        private Author selectedAuthor;

        #region CONSTRUCTORS

        public AddEditAuthor()
        {
            selectedAuthor = new Author();

            InitializeComponent();

            this.Title = "Add an author";
            this.BtOk.Content = "Add";

            this.CbNationality.ItemsSource = Enum.GetNames(typeof(SevenLib.Helpers.Nationality));
        }

        public AddEditAuthor(Author authorToEdit)
        {
            selectedAuthor = new Author(authorToEdit);

            InitializeComponent();
            
            this.Title = "Edit an author";
            this.BtOk.Content = "Edit";

            this.CbNationality.ItemsSource = Enum.GetNames(typeof(SevenLib.Helpers.Nationality));

            this.TbLastName.Text = selectedAuthor.LastName;
            this.TbFirstName.Text = selectedAuthor.FirstName;
            this.DpBirthDate.Text = selectedAuthor.BirthDate.ToLongDateString();

            if (selectedAuthor.DeathDate != null)
                this.DpDeathDate.Text = DateTime.Parse(selectedAuthor.DeathDate.ToString()).ToLongDateString();

            this.CbNationality.SelectedItem = this.CbNationality.Items.GetItemAt(CbNationality.Items.IndexOf(selectedAuthor.Nationality.ToString()));
        }

        #endregion

        #region METHODS

        public Author GetSelectedAuthor()
        {
            return selectedAuthor;
        }

        private bool CheckUserInput()
        {
            if (String.IsNullOrEmpty(TbLastName.Text))
            {
                MessageBox.Show("Invalid Lastname", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(TbFirstName.Text))
            {
                MessageBox.Show("Invalid Firstname", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(DpBirthDate.Text))
            {
                MessageBox.Show("Invalid Birthdate", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(CbNationality.Text))
            {
                MessageBox.Show("Invalid Nationality", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        #endregion

        #region GUI OPERATIONS

        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckUserInput())
                return;

            selectedAuthor.LastName = TbLastName.Text;
            selectedAuthor.FirstName = TbFirstName.Text;
            selectedAuthor.BirthDate = DateTime.Parse(DpBirthDate.Text);
            selectedAuthor.DeathDate = String.IsNullOrEmpty(DpDeathDate.Text) ? (DateTime?)null : DateTime.Parse(DpDeathDate.Text);
            selectedAuthor.Nationality = (SevenLib.Helpers.Nationality)Enum.Parse(typeof(SevenLib.Helpers.Nationality), CbNationality.Text);

            this.DialogResult = true;
            this.Close();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion
    }
}
