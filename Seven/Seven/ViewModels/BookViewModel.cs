using SevenDB;
using SevenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Seven
{
    class BookViewModel
    {
        public RepositoryBook rb;
        public RepositoryCopy rc;

        public List<Book> books;

        #region CONSTRUCTORS

        public BookViewModel()
        {
            rb = new RepositoryBook();
            rc = new RepositoryCopy();
            
            refreshBooks();
        }

        #endregion

        #region METHODS

        public void refreshBooks()
        {
            RefreshRepositories();
            books = rb.GetBooks().ToList<Book>();
        }

        public List<Book> searchBook(String title)
        {
            RefreshRepositories();
            return rb.GetBooksByBeginningOfTitle(title).ToList<Book>();
        }

        private void RefreshRepositories()
        {
            rb.ConnectionString = SevenLib.Helpers.Const.DBPath;
            rc.ConnectionString = SevenLib.Helpers.Const.DBPath;
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddBook(Book book)
        {
            RefreshRepositories();
            return rb.AddBook(book);
        }

        public bool EditBook(Book book)
        {
            RefreshRepositories();
            return rb.EditBook(book);
        }

        public bool DeleteBook(Book book)
        {
            RefreshRepositories();
            return rb.DeleteBookByID((int)book.ID);
        }

        public bool AddCopy(Book book)
        {
            RefreshRepositories();
            return rc.AddCopy(new Copy(book));
        }

        public Int64 GetNbrOfCopy(Book book)
        {
            RefreshRepositories();
            return rc.GetNumberOfCopies(book.ID);
        }
        #endregion
    }
}
