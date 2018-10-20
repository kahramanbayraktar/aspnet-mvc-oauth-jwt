using AspNetMvcOAuthJwt.AuthorizationServer;
using AspNetMvcOAuthJwt.AuthorizationServer.Formats;
using AspNetMvcOAuthJwt.AuthorizationServer.Helpers;
using AspNetMvcOAuthJwt.AuthorizationServer.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace AspNetMvcOAuthJwt.AuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = WebConfigValues.OAuthAllowInsecureHttp,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(WebConfigValues.OAuthAccessTokenExpireTimeSpan),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["AuthServerUrl"])
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}