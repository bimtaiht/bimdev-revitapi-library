using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Dict<T> : IList<T>
    {
        protected List<T>? items;
        protected virtual List<T> StorageItems
        {
            get => this.items ??= new List<T>();
            set => this.items = value;
        }
        public virtual List<T> Items
        {
            get => this.StorageItems;
            set => this.StorageItems = value;
        }

        public virtual T this[object key]
        {
            get
            {
                if (typeof(T).BaseType.Name.ToLower().Contains("base"))
                {
                    return this.Items.FirstOrDefault(x => key.Equals(((dynamic)x).Key));
                }
                else
                {
                    return default;
                }

            }
        }

        public int Count => this.Items.Count;

        public bool IsReadOnly => false;

        public virtual bool IsAutoAddItemWhenRetrieveIndex => false;

        protected Func<int, T> getItemByIndex;
        public virtual Func<int, T> GetItemByIndex
        {
            get => this.getItemByIndex ??= this.GetGetItemByIndex();
            set => this.getItemByIndex = value;
        }

        public T this[int index]
        {
            get => this.GetItemByIndex(index);
            set
            {
                this.Items[index] = value;
                this.OnSetItem?.Invoke(index, value);
            }
        }

        public Action<int, T>? OnSetItem { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return this.Items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
        }

        public Action<T>? OnAddItem { get; set; }

        public void Add(T item)
        {
            this.Items.Add(item);
            this.OnAddItem?.Invoke(item);
        }

        public void Clear()
        {
            this.Items.Clear();
        }

        public bool Contains(T item)
        {
            return this.Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Items.CopyTo(array, arrayIndex);
        }

        public Action<T>? OnRemoveItem { get; set; }

        public bool Remove(T item)
        {
            var flag = this.Items.Remove(item);
            this.OnRemoveItem?.Invoke(item);
            return flag;
        }
    }
}
