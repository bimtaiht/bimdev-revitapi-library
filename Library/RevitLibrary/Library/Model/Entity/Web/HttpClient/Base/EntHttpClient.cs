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
    public class EntHttpClient : IDisposable
    {
        private HttpClient? httpClient;
        public HttpClient HttpClient => this.httpClient ??= this.GetHttpClient();
        
        public HttpMethod Method { get; set; }
        
        public string? Url { get; set; }

        public string? BearerToken { get; set; }

        // request
        private object? requestData;
        public object RequestData
        {
            get => this.requestData ??= new { };
            set => this.requestData = value;
        }

        private StringContent? requestContent;
        public StringContent RequestContent => this.requestContent ??= this.GetRequestContent();

        private RequestCookieDict? requestCookieDict;
        public RequestCookieDict RequestCookieDict => this.requestCookieDict ??= new RequestCookieDict();

        // reponse
        private HttpResponseMessage? reponseMessage;
        public HttpResponseMessage ReponseMessage => this.reponseMessage ??= this.GetReponseMessage();

        public System.Net.HttpStatusCode StatusCode => this.ReponseMessage.StatusCode;

        public bool IsSuccessStatusCode => this.ReponseMessage.IsSuccessStatusCode;

        private HttpResponseData? reponseData;
        public HttpResponseData ReponseData => this.reponseData ??= this.GetReponseData();

        private ReponseCookieDict? reponseCookieDict;
        public ReponseCookieDict ReponseCookieDict => this.reponseCookieDict ??= new ReponseCookieDict
        {
            HttpClient = this
        };

        public void Dispose()
        {
            this.ReponseMessage.Dispose();
        }
    }
}
