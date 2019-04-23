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
        public RepositoryAuthor ra;

        public List<Author> authors;

        #region CONSTRUCTORS

        public AuthorViewModel()
        {
            ra = new RepositoryAuthor();
            
            refreshAuthors();
        }

        #endregion

        #region METHODS

        public void refreshAuthors()
        {
            refreshRepository();
            authors = ra.GetAuthors().ToList<Author>();
        }

        public List<Author> searchAuthor(String lastname)
        {
            refreshRepository();
            return ra.GetAuthorsByBeginningOfLastName(lastname).ToList<Author>();
        }

        private void refreshRepository()
        {
            ra.ConnectionString = SevenLib.Helpers.Const.DBPath;
        }

        #endregion

        #region DATABASE OPERATIONS

        public bool AddAuthor(Author author)
        {
            refreshRepository();
            return ra.AddAuthor(author);
        }

        public bool EditAuthor(Author author)
        {
            refreshRepository();
            return ra.EditAuthor(author);
        }

        public bool DeleteAuthor(Author author)
        {
            refreshRepository();
            return ra.DeleteAuthorByID((int)author.ID);
        }

        #endregion
    }
}
