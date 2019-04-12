using SevenLib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Author
    {
        #region CONSTRUCTORS

        public Author() { }

        public Author(String lastname, String firstname, DateTime birthDate, Nationality nationality, DateTime? deathDate = null, Int64? id = null)
        {
            this.ID = id;
            this.LastName = lastname;
            this.FirstName = firstname;
            this.BirthDate = birthDate;
            this.DeathDate = deathDate;
            this.Nationality = nationality;
        }

        public Author(Author authorToCopy)
        {
            this.ID = authorToCopy.ID;
            this.LastName = authorToCopy.LastName;
            this.FirstName = authorToCopy.FirstName;
            this.BirthDate = authorToCopy.BirthDate;
            this.DeathDate = authorToCopy.DeathDate;
            this.Nationality = authorToCopy.Nationality;
        }

        #endregion


        #region GETTERS/SETTERS
        
        public Int64? ID { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public Nationality Nationality { get; set; }

        #endregion


        #region METHODS

        public override String ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }

        public String GetFullAuthor()
        {
            String authorStr = String.Format("'{0} {1}' \n--> Born the : {2}", FirstName, LastName, BirthDate.ToLongDateString());

            if (DeathDate != null)
                authorStr += String.Format("\n--> Died the : {0}", DeathDate.ToString());

            authorStr += String.Format("\n--> Nationality : {0} - {1}", Nationality.ToString(), EnumHelpers.ToDescriptionString(Nationality));

            return authorStr;
        }

        #endregion
    }
}
