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
    public static class ConfigLoaderFromFileUtil
    {
        public static Func<T> GetTryLoadConfig<T>(this ConfigLoaderFromFile<T> q) where T : Config<T>
        {
            return () => File_Util.ReadTxtFile(q.Path)!.JsonDeserialize<T>();
        }

        public static Action<string> GetSaveConfig<T>(this ConfigLoaderFromFile<T> q) where T : Config<T>
        {
            return (configString) => File_Util.WriteTxtFile(q.Path!, configString, true);
        }
    }
}
