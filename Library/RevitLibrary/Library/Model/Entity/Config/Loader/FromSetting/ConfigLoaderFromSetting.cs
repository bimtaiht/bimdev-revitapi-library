using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ConfigLoaderFromSetting<T> : ConfigLoader<T> where T : Config<T>
    {
        public ConfigLoaderFromSetting(System.Configuration.ApplicationSettingsBase setting, string storageKey)
        {
            this.Setting = setting;
            this.StorageKey = storageKey;
        }

        public System.Configuration.ApplicationSettingsBase Setting { get; set; }

        private dynamic SettingDynamic => this.Setting;

        public string StorageKey { get; set; }

        public string StorageString
        {
            get => (string)this.SettingDynamic[StorageKey];
            set => this.SettingDynamic[StorageKey] = value;
        }

        public override Func<T> TryLoadConfig => this.tryLoadConfig ??= this.GetTryLoadConfig();

        public override Action<string> SaveConfig => this.saveConfig ??= this.GetSaveConfig();
    }
}
