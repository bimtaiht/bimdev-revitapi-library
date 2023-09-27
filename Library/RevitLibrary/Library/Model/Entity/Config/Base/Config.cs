using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Config<T> where T : Config<T>
    {
        [JsonIgnore]
        public ConfigLoader<T>? Loader { get; set; }
    }
}
