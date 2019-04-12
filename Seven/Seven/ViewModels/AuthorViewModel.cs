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
    class AuthorViewModel
    {
        public RepositoryAuthor repository;

        public List<Author> authors;

        #region CONSTRUCTORS

        public AuthorViewModel()
        {
            repository = new RepositoryAuthor();

            refreshAuthors();
        }

        #endregion

        #region METHODS

        public void refreshAuthors()
        {
            authors = repository.GetAuthors().ToList<Author>();
        }

        public List<Author> searchAuthor(String lastname)
        {
            return repository.GetAuthorsByBeginningOfLastName(lastname).ToList<Author>();
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddAuthor(Author author)
        {
            return repository.AddAuthor(author);
        }

        public bool EditAuthor(Author author)
        {
            return repository.EditAuthor(author);
        }

        public bool DeleteAuthor(Author author)
        {
            return repository.DeleteAuthorByID((int)author.ID);
        }

        #endregion
    }
}
