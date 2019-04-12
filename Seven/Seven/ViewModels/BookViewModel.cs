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
        public RepositoryBook repository;

        public List<Book> books;

        #region CONSTRUCTORS

        public BookViewModel()
        {
            repository = new RepositoryBook();

            refreshBooks();
        }

        #endregion

        #region METHODS

        public void refreshBooks()
        {
            books = repository.GetBooks().ToList<Book>();
        }

        public List<Book> searchBook(String title)
        {
            return repository.GetBooksByBeginningOfTitle(title).ToList<Book>();
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddBook(Book book)
        {
            return repository.AddBook(book);
        }

        public bool EditBook(Book book)
        {
            return repository.EditBook(book);
        }

        public bool DeleteBook(Book book)
        {
            return repository.DeleteBookByID((int)book.ID);
        }

        #endregion
    }
}
