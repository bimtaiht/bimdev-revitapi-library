using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public abstract class ConfigLoader<T> where T : Config<T>
    {
        public bool IsLoaded { get; set; } = false;

        protected Func<T>? tryLoadConfig;
        public abstract Func<T> TryLoadConfig { get; }

        private T? config;
        public T Config => this.config ??= this.GetConfig();

        protected Action<string>? saveConfig;
        public abstract Action<string> SaveConfig { get; }
    }
}
