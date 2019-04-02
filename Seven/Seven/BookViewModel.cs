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
    class BookViewModel : UserControl
    {
        public RepositoryBook repository;

        public List<Book> books;
        
        public BookViewModel()
        {
            repository = new RepositoryBook();

            books = repository.GetBooks().ToList<Book>();
        }

    }
}
