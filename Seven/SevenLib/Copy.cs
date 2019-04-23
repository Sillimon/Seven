using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Copy
    {
        public Copy(Book book, bool borrowed = false, Int64? reference = null)
        {
            this.Reference = reference;
            this.Borrowed = borrowed;
            this.Book = book;
        }

        #region GETTERS/SETTERS

        public Int64? Reference { get; set; }

        public bool Borrowed { get; set; }

        public Book Book { get; set; }

        #endregion

        #region METHODS

        public override String ToString()
        {
            return String.Format("Copy Reference : {0} \nBook : {1}", Reference, Book.Title);
        }

        #endregion

    }
}
