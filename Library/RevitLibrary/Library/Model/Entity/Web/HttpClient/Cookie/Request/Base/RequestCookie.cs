using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RequestCookie : Base<RequestCookie, RequestCookieDict>
    {
        public string Value { get; set; }

        public Cookie Cookie => new((string)this.Key, this.Value);
    }
}
