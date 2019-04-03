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
        
        public BookViewModel()
        {
            repository = new RepositoryBook();

            books = repository.GetBooks().ToList<Book>();
        }

        public List<Book> searchBook(String title)
        {
            books = repository.GetBooksByBeginningOfTitle(title).ToList<Book>();
            return books;
        }

    }
}
