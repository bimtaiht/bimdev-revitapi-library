using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class WebAPIFlowGroup
    {
        public string? BaseUrl { get; set; }

        public string? AuthRoute { get; set; } = "auth";

        public string AuthUrl => $"{this.BaseUrl}/{this.AuthRoute}";

        public Func<string>? GetAccessToken { get; set; }

        public Func<string>? GetRefreshToken { get; set; }

        public Action<string, string>? HandleToken { get; set; }
    }
}
