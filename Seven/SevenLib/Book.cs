using SevenLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Book
    {
        #region CONSTRUCTORS

        public Book() { }

        public Book(String title, DateTime date, Genre genre , String summary, Author author = null)
        {
            this.Title = title;
            this.Date = date;
            this.Genre = genre;
            this.Summary = summary;
            this.Author = author;
        }

        public Book(Book BookToCopy)
        {
            this.Title = BookToCopy.Title;
            this.Date = BookToCopy.Date;
            this.Genre = BookToCopy.Genre;
            this.Summary = BookToCopy.Summary;
            this.Author = BookToCopy.Author;
        }

        #endregion

        #region GETTERS/SETTERS

        public String Title { get; set; }

        public DateTime Date { get; set; }

        public Genre Genre { get; set; }

        public String Summary { get; set; }

        public Author Author { get; set; }

        #endregion


        #region METHODS

        public override string ToString()
        {
            return String.Format("'{0}' \n--> Release date : {1}" +
                "\n--> Genre : {2}" +
                "\n--> Author : {3}" +
                "\n--> Summary : {4}",
                Date.ToLongDateString(),
                nameof(Genre),
                Author.FirstName + " " + Author.LastName,
                Summary);
        }

        #endregion
    }
}
