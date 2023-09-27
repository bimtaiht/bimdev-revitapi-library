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
    public static class WebAPIFlowGroupUtil
    {
        public static HttpResponseData Call(this WebAPIFlowGroup q, WebAPIFlow flow)
        {
            if (flow.Group == null)
            {
                flow.Group = q;
            }

            return flow.ReponseData;
        }

        public static bool Login(this WebAPIFlowGroup q, object requestData)
        {
            var client = new EntHttpClient
            {
                Url = $"{q.AuthUrl}/login",
                RequestData = requestData,
                Method = HttpMethod.POST,
            };

            if (client.IsSuccessStatusCode)
            {
                string accessToken = client.ReponseData.data.accessToken;
                var refreshToken = client.ReponseCookieDict["refreshToken"].Value;
                q.HandleToken!(accessToken, refreshToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Logout(this WebAPIFlowGroup q)
        {
            var client = new EntHttpClient
            {
                Url = $"{q.AuthUrl}/logout",
                Method = HttpMethod.POST
            };

            var data = client.ReponseData;
            q.HandleToken!("", "");
            return true;
        }
    }
}
