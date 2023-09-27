using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ReponseCookieDict : Dict<ReponseCookie>
    {
        public override List<ReponseCookie> Items => this.items ??= this.GetItems();

        public EntHttpClient? HttpClient { get; set; }
    }
}
