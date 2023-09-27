using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Base<T, TDict> where TDict : Dict<T>
    {
        public virtual object Key { get; set; }

        protected virtual TDict? Dict_Storage { get; set; }
        public TDict Dict
        {
            get => this.Dict_Storage!;
            set => this.Dict_Storage = value;
        }

        public List<T> CurrentList => this.Dict.Items;

        public int Index => this.CurrentList.IndexOf((T)(dynamic)this);

        public virtual bool IsFirst => this.Index == 0;

        public virtual bool IsLast => this.Index == this.CurrentList.Count - 1;

        public T? Prev => !this.IsFirst ? this.CurrentList[this.Index - 1] : default;

        public T? Next => !this.IsLast ? this.CurrentList[this.Index + 1] : default;
    }
}
