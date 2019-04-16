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
            books = rb.GetBooks().ToList<Book>();
        }

        public List<Book> searchBook(String title)
        {
            return rb.GetBooksByBeginningOfTitle(title).ToList<Book>();
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddBook(Book book)
        {
            return rb.AddBook(book);
        }

        public bool EditBook(Book book)
        {
            return rb.EditBook(book);
        }

        public bool DeleteBook(Book book)
        {
            return rb.DeleteBookByID((int)book.ID);
        }

        public bool AddCopy(Book book)
        {
            return rc.AddCopy(new Copy(book));
        }

        public Int64 GetNbrOfCopy(Book book)
        {
            return rc.GetNumberOfCopies(book.ID);
        }
        #endregion
    }
}
