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
    public static class ConfigLoaderFromSettingUtil
    {
        public static Func<T> GetTryLoadConfig<T>(this ConfigLoaderFromSetting<T> q) where T : Config<T>
        {
            return () => q.StorageString.JsonDeserialize<T>();
        }

        public static Action<string> GetSaveConfig<T>(this ConfigLoaderFromSetting<T> q) where T : Config<T>
        {
            return (configString) =>
            {
                q.StorageString = configString;
                q.Setting.Save();
            };
        }
    }
}
