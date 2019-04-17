using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SevenLib.Helpers
{
    public class HashHelper
    {
        const String salt = "iloveturtles";
        const String pepper = "iliketrains";

        public static String HashString(String plaintext)
        {
            plaintext = salt + plaintext + pepper;

            byte[] bytes = Encoding.UTF8.GetBytes(plaintext);

            SHA256Managed hashString = new SHA256Managed();
            byte[] hash = hashString.ComputeHash(bytes);

            String hashed = String.Empty;

            foreach (byte B in hash)
            {
                hashed += String.Format("{0:x2}", B);
            }

            return hashed;
        }
    }
}
