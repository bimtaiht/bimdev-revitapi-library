using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ConfigLoaderFromFile<T> : ConfigLoader<T> where T : Config<T>
    {
        public ConfigLoaderFromFile(string path)
        {
            this.Path = path;
        }

        public string Path { get; set; }

        public override Func<T> TryLoadConfig => this.tryLoadConfig ??= this.GetTryLoadConfig();

        public override Action<string> SaveConfig => this.saveConfig ??= this.GetSaveConfig();
    }
}
