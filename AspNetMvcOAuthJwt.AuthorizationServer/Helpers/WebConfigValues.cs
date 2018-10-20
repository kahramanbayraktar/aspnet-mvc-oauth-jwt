using System;
using System.Configuration;

namespace AspNetMvcOAuthJwt.AuthorizationServer.Helpers
{
    public class WebConfigValues
    {
        public static bool OAuthAllowInsecureHttp => Convert.ToBoolean(ConfigurationManager.AppSettings["OAuthAllowInsecureHttp"]);

        public static int OAuthAccessTokenExpireTimeSpan => Convert.ToInt32(ConfigurationManager.AppSettings["OAuthAccessTokenExpireTimeSpanInMins"]);
    }
}