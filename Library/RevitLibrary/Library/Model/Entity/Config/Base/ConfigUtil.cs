using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public static class ConfigUtil
    {
        public static void Save<T>(this Config<T> q) where T: Config<T>
        {
            var loader = q.Loader;
            if (loader == null || !loader.IsLoaded) return;

            var configString = JsonConvert.SerializeObject(q, Formatting.Indented);
            loader.SaveConfig?.Invoke(configString);
        }

        public static void SetValue<T>(this Config<T> q, Action action, bool isSave) where T : Config<T>
        {
            action();
            if (isSave)
            {
                q.Save();
            }
        }
    }
}
