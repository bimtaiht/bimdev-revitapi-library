using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class AuthTokenConfig : Config<AuthTokenConfig>
    {
        private string? _accessToken;
        public string? accessToken
        {
            get => this._accessToken;
            set
            {
                this._accessToken = value;
            }
        }

        private string? _refreshToken;
        public string? refreshToken
        {
            get => this._refreshToken;
            set
            {
                this._refreshToken = value;
            }
        }
    }
}
