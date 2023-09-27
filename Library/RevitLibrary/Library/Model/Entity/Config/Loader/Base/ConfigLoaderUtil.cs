using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public static class ConfigLoaderUtil
    {
        public static T GetConfig<T>(this ConfigLoader<T> q) where T : Config<T>
        {
            T config = default(T);
            try
            {
                config = q.TryLoadConfig();
            }
            catch
            {

            }

            if (config == null)
            {
                config = (T)Activator.CreateInstance(typeof(T), new object[] { });
            }

            config.Loader = q;
            q.IsLoaded = true;
            return config;
        }
    }
}
