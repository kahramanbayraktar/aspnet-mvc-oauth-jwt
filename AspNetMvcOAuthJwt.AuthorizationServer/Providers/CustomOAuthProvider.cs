using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetMvcOAuthJwt.AuthorizationServer.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private OAuthGrantResourceOwnerCredentialsContext _credentialsContext;

        // Checks if ClientId is valid.
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (!context.TryGetBasicCredentials(out string clientId, out string clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "ClientId is no set");
                return Task.FromResult<object>(null);
            }

            var audience = Common.Helpers.AudienceHelper.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_client", $"Invalid ClientId '{context.ClientId}'");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            _credentialsContext = context;

            if (CheckUserCredentials())
            {
                var authTicket = GetAuthenticationTicket();

                _credentialsContext.Validated(authTicket);

                return Task.FromResult<object>(null);
            }

            context.SetError("invalid_grant", "User name or password is incorrect");
            return Task.FromResult<object>(null);
        }

        // Db operation. Must be moved to an appropriate namespace/tier.
        private bool CheckUserCredentials()
        {
            if (_credentialsContext.UserName.ToLower() == "abc@def.com" && _credentialsContext.Password == "123456")
                return true;
            return false;
        }

        private AuthenticationTicket GetAuthenticationTicket()
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, _credentialsContext.UserName.ToLower()));
            identity.AddClaim(new Claim("sub", _credentialsContext.UserName.ToLower()));
            identity.AddClaim(new Claim("role", "Standard User"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "audience", _credentialsContext.ClientId }
                });

            var ticket = new AuthenticationTicket(identity, props);

            return ticket;
        }
    }
}