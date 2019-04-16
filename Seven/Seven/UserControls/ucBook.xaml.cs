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
    /// Logique d'interaction pour ucBook.xaml
    /// </summary>
    public partial class ucBook : UserControl
    {
        BookViewModel bookModel;

        public ucBook()
        {
            InitializeComponent();

            bookModel = new BookViewModel();

            dgvBooks.ItemsSource = bookModel.books;
            refreshBooks();
        }

        #region METHODS

        public void refreshBooks()
        {
            bookModel.refreshBooks();
            dgvBooks.ItemsSource = bookModel.books;
            dgvBooks.Items.Refresh();
        }

        public void refreshCopies(Book book)
        {
            this.SelectedBookNumberOfCopy.Text = bookModel.GetNbrOfCopy(book).ToString();
        }

        #endregion


        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DataContext = bookModel;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            dgvBooks.ItemsSource = bookModel.searchBook(tbSearch.Text);
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditBook addWindow = new AddEditBook();

            addWindow.ShowDialog();

            if(addWindow.DialogResult == true)
            {
                if (bookModel.AddBook(addWindow.GetSelectedBook()))
                {
                    MessageBox.Show("Book was added successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    refreshBooks();
                }
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var b in dgvBooks.SelectedItems)
                {
                    Book book = b as Book;

                    bookModel.DeleteBook(book);
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
                refreshBooks();
            }
        }
        
        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgvBooks.SelectedItem == null)
            {
                MessageBox.Show("Select a book to modify", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddEditBook editWindow = new AddEditBook((Book)dgvBooks.SelectedItem);

            editWindow.ShowDialog();

            if (editWindow.DialogResult == true)
            {
                if (bookModel.EditBook(editWindow.GetSelectedBook()))
                {
                    MessageBox.Show("Book was modified successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    refreshBooks();
                }
            }
        }


        private void DgvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvBooks.SelectedItem == null)
                return;

            Book selectedBook = (Book)dgvBooks.SelectedItem;

            this.SelectedBookTitle.Text = selectedBook.Title;
            this.SelectedBookDate.Text = selectedBook.Date.ToLongDateString();
            this.SelectedBookGenre.Text = Enum.GetName(typeof(SevenLib.Helpers.Genre), selectedBook.Genre);
            this.SelectedBookSummary.Text = selectedBook.Summary;
            this.SelectedBookAuthor.Text = selectedBook.Author.ToString();

            refreshCopies(selectedBook);
        }

        private void BtAddCopy_Click(object sender, RoutedEventArgs e)
        {
            if (dgvBooks.SelectedItem == null)
                return;

            Book selectedBook = (Book)dgvBooks.SelectedItem;

            bookModel.AddCopy(selectedBook);

            refreshCopies(selectedBook);
        }
    }
}
