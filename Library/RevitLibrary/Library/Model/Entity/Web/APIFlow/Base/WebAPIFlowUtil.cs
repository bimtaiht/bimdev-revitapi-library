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
    public static class WebAPIFlowUtil
    {
        public static HttpResponseData GetReponseData(this WebAPIFlow q)
        {
            var isAuthenticated = q.IsAuthenticated;

            Func<EntHttpClient> getMainClient = () => new EntHttpClient
            {
                Url = q.Url,
                BearerToken = isAuthenticated ? q.GetAccessToken() : null,
                RequestData = q.RequestData,
                Method = q.Method
            };

            var mainClient = getMainClient();
            var responseData = mainClient.ReponseData;
            if (mainClient.IsSuccessStatusCode)
            {
                return responseData;
            }
            else
            {
                if (isAuthenticated)
                {
                    switch (mainClient.StatusCode)
                    {
                        case System.Net.HttpStatusCode.Unauthorized:
                            {
                                // check refreshToken
                                var originRefreshToken = q.GetRefreshToken();
                                if (originRefreshToken == null || originRefreshToken.Length == 0)
                                {
                                    return new HttpResponseData { success = false };
                                }

                                // call requestRefreshToken
                                var refreshTokenClient = new EntHttpClient
                                {
                                    Url = $"{q.AuthUrl}/refreshToken",
                                    Method = HttpMethod.POST
                                };
                                refreshTokenClient.RequestCookieDict["refreshToken"].Value = originRefreshToken;
                                if (refreshTokenClient.IsSuccessStatusCode)
                                {
                                    string accessToken = refreshTokenClient.ReponseData.data;
                                    var refreshToken = refreshTokenClient.ReponseCookieDict["refreshToken"].Value;
                                    q.HandleToken(accessToken, refreshToken);

                                    return getMainClient().ReponseData;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                return responseData;
            }
        }
    }
}
