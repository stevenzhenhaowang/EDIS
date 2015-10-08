using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Services
{
    public class IDUtilities
    {
        public static string getUniqueFriendlyMemberIdFromInt(int id)
        {
            string idstring = id.ToString();
            string placeHolder = "0000000000";
            int lengthOfId = placeHolder.Length;
            if (idstring.Length > lengthOfId)
            {
                throw new Exception("Id length exceeded range of 10");
            }
            return placeHolder.Substring(0, lengthOfId - idstring.Length) + idstring;

        }
    }
}