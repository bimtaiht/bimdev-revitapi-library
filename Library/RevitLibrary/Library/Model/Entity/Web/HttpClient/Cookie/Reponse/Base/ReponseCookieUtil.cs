using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public static class ReponseCookieUtil
    {
        public static List<string> GetKeyValue(this ReponseCookie q)
        {
            var mainContent = q.RawContent!.Split(';')[0];
            var splitIndex = mainContent.IndexOf('=');

            return new List<string>
            {
                mainContent.Substring(0, splitIndex),
                mainContent.Substring(splitIndex+1, mainContent.Length - (splitIndex + 1))
            };
        }
    }
}
