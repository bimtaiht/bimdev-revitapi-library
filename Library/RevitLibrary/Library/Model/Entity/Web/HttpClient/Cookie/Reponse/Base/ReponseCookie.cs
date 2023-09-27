using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ReponseCookie : Base<ReponseCookie, ReponseCookieDict>
    {
        public string? RawContent { get; set; }

        private List<string>? keyValue;
        public List<string> KeyValue => this.keyValue ??= this.GetKeyValue();

        public override object Key => this.KeyValue[0];

        public string Value => this.KeyValue[1];
    }
}
