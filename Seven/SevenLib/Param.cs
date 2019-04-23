using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib
{
    public class Param
    {
        #region CONSTRUCTORS

        public Param() { }

        public Param(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region GETTERS/SETTERS

        public String Name { get; set; }

        public String Value { get; set; }

        #endregion
    }
}
