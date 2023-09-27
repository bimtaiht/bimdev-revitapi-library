using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Model.Entity
{
    public static class EntHttpClientUtil
    {
        public static HttpClient GetHttpClient(this EntHttpClient q)
        {
            HttpClient httpClient = null;

            var requestCookies = q.RequestCookieDict.Items;
            if (requestCookies.Any())
            {
                var uri = new Uri(q.Url);
                var cookieContainer = new CookieContainer();
                foreach (var requestCookie in requestCookies)
                {
                    cookieContainer.Add(uri, requestCookie.Cookie);
                }

                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer
                };

                httpClient = new HttpClient(handler);
            }
            else
            {
                httpClient = new HttpClient();
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));

            var bearerToken = q.BearerToken;
            if (bearerToken != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            return httpClient;
        }

        public static StringContent GetRequestContent(this EntHttpClient q)
        {
            var requestString = q.RequestData!.JsonSerialize();
            return new StringContent(requestString, UnicodeEncoding.UTF8, "application/json");
        }

        public static HttpResponseMessage GetReponseMessage(this EntHttpClient q)
        {
            var url = q.Url;
            var httpClient = q.HttpClient;

            Task<HttpResponseMessage>? responseTask = null;
            switch (q.Method)
            {
                case HttpMethod.GET:
                    responseTask = httpClient.GetAsync(url);
                    break;
                case HttpMethod.POST:
                    responseTask = httpClient.PostAsync(url, q.RequestContent);
                    break;
                case HttpMethod.PUT:
                    responseTask = httpClient.PutAsync(url, q.RequestContent);
                    break;
            }

            return responseTask!.GetAwaiter().GetResult();
        }

        public static HttpResponseData GetReponseData(this EntHttpClient q)
        {
            var reponseData = q.ReponseMessage.Content.ReadAsAsync<HttpResponseData>().GetAwaiter().GetResult();
            q.Dispose();
            return reponseData;
        }
    }
}
