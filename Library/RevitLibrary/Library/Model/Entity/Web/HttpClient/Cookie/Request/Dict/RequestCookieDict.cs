using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RequestCookieDict : Dict<RequestCookie>
    {
        public override List<RequestCookie> Items => this.items ??= new List<RequestCookie>();

        public override RequestCookie this[object key]
        {
            get
            {
                var items = this.Items;
                var item = items.FirstOrDefault(x => x.Key == key);
                if (item == null)
                {
                    item = new RequestCookie
                    {
                        Dict = this,
                        Key = key
                    };
                    items.Add(item);
                }
                return item;
            }
        }
    }
}
