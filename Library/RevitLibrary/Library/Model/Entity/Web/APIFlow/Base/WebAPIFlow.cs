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
    public class WebAPIFlow
    {
        public WebAPIFlowGroup? Group { get; set; }

        public bool IsAuthenticated { get; set; } = true;

        public string BaseUrl => this.Group!.BaseUrl!;

        public string AuthRoute => this.Group!.AuthRoute!;

        public Func<string> GetAccessToken => this.Group!.GetAccessToken!;

        public Func<string> GetRefreshToken => this.Group!.GetRefreshToken!;

        public Action<string, string> HandleToken => this.Group!.HandleToken!;

        public HttpMethod Method { get; set; }

        public string? Route { get; set; }

        public string AuthUrl => this.Group!.AuthUrl!;

        public string Url => $"{this.BaseUrl}/{this.Route}";

        private object? requestData;
        public object RequestData
        {
            get => this.requestData ??= new { };
            set => this.requestData = value;
        }

        private HttpResponseData? reponseData;
        public HttpResponseData ReponseData => this.reponseData ??= this.GetReponseData();
    }
}
