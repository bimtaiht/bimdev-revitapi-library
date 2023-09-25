using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public static class DictUtil
    {
        public static Func<int, T> GetGetItemByIndex<T>(this Dict<T> q)
        {
            var list = q.Items;
            if (!q.IsAutoAddItemWhenRetrieveIndex)
            {
                return (index) => list[index];
            }
            else
            {
                return (index) =>
                {
                    var count = list.Count;
                    var delta = count - 1 - index;
                    if (delta < 0)
                    {
                        var absDelta = Math.Abs(delta);
                        for (int i = 0; i < absDelta; i++)
                        {
                            var newItem = q.CreateItem();
                            list.Add(newItem);
                        }
                    }
                    return list[index];
                };
            }
        }

        public static T CreateItem<T>(this Dict<T> q)
        {
            var newItem = (T)Activator.CreateInstance(typeof(T), new object[] { });
            dynamic newItem_any = newItem;
            try
            {
                newItem_any.Dict = (dynamic)q;
            }
            catch
            {

            }

            return newItem;
        }

        public static Dict<T> ToDict<T>(this IEnumerable<T> items)
        {
            return new Dict<T>
            {
                Items = items.ToList()
            };
        }
    }
}
