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
    /// Logique d'interaction pour AddEditBook.xaml
    /// </summary>
    public partial class AddEditBook : Window
    {
        private Book selectedBook;

        private RepositoryAuthor ra;

        #region CONSTRUCTORS

        public AddEditBook()
        {
            selectedBook = new Book();

            ra = new RepositoryAuthor();

            InitializeComponent();

            this.Title = "Add a book";
            this.BtOk.Content = "Add";

            this.CbGenre.ItemsSource = Enum.GetNames(typeof(SevenLib.Helpers.Genre));
            this.CbAuthor.ItemsSource = ra.GetAuthors();
        }

        public AddEditBook(Book bookToEdit)
        {    
            selectedBook = new Book(bookToEdit);

            ra = new RepositoryAuthor();

            InitializeComponent();
            
            this.Title = "Edit a book";
            this.BtOk.Content = "Edit";

            this.CbGenre.ItemsSource = Enum.GetNames(typeof(SevenLib.Helpers.Genre));
            this.CbAuthor.ItemsSource = ra.GetAuthors();

            this.TbTitle.Text = selectedBook.Title;
            this.DpDate.Text = selectedBook.Date.ToLongDateString();
            this.CbGenre.SelectedItem = this.CbGenre.Items.GetItemAt(CbGenre.Items.IndexOf(selectedBook.Genre.ToString()));
            this.TbSummary.Text = selectedBook.Summary;
            //this.CbAuthor.SelectedItem = this.CbAuthor.Items.GetItemAt(CbAuthor.Items.IndexOf(selectedBook.Author.ToString()));

            //TEST//

            foreach (var a in CbAuthor.ItemsSource)
            {
                if (a.ToString() == selectedBook.Author.ToString())
                {
                    this.CbAuthor.SelectedItem = a;
                }
            }
        }

        #endregion

        #region METHODS

        public Book GetSelectedBook()
        {
            return selectedBook;
        }

        private bool CheckUserInput()
        {
            if (String.IsNullOrEmpty(TbTitle.Text))
            {
                MessageBox.Show("Invalid Title", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(DpDate.Text))
            {
                MessageBox.Show("Invalid Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(CbGenre.Text))
            {
                MessageBox.Show("Invalid Genre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(CbGenre.Text))
            {
                MessageBox.Show("Invalid Genre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(TbSummary.Text))
            {
                MessageBox.Show("Invalid Summary", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (String.IsNullOrEmpty(CbAuthor.Text))
            {
                MessageBox.Show("Invalid Author", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            selectedBook.Title = TbTitle.Text;
            selectedBook.Date = DateTime.Parse(DpDate.Text);
            selectedBook.Genre = (SevenLib.Helpers.Genre)Enum.Parse(typeof(SevenLib.Helpers.Genre), CbGenre.Text);
            selectedBook.Summary = TbSummary.Text;
            selectedBook.Author = (Author)CbAuthor.SelectedItem;

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
