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
    public static class ReponseCookieDictUtil
    {
        public static List<ReponseCookie> GetItems(this ReponseCookieDict q)
        {
            var response = q.HttpClient!.ReponseMessage;
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                return cookieValues.Select(x => new ReponseCookie
                {
                    Dict = q,
                    RawContent = x
                }).ToList();
            }
            else
            {
                return new List<ReponseCookie>();
            }
        }
    }
}
