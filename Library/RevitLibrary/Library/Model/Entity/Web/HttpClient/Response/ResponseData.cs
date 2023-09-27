using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class HttpResponseData
    {
        public bool success { get; set; }

        public dynamic data { get; set; }
    }
}
