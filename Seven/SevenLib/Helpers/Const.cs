using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib.Helpers
{
    public static class Const
    {
        public static String DBPath
        {
            get { return ConfigurationManager.AppSettings["connectionString"]; }
            set { ConfigurationManager.AppSettings["connectionString"] = value; }
        }
    }
}
