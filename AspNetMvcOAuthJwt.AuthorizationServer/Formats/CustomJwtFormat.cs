using System;
using System.IdentityModel.Tokens;
using AspNetMvcOAuthJwt.Common.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace AspNetMvcOAuthJwt.AuthorizationServer.Formats
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            string audienceId = data.Properties.Dictionary.ContainsKey("audience")
                ? data.Properties.Dictionary["audience"]
                : null;

            if (string.IsNullOrWhiteSpace(audienceId))
                throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            // Get application from db
            Application audience = Common.Helpers.AudienceHelper.FindAudience(audienceId);

            var keyByteArray = TextEncodings.Base64Url.Decode(audience.ClientSecret);
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims,
                data.Properties.IssuedUtc.Value.UtcDateTime, data.Properties.ExpiresUtc.Value.UtcDateTime, signingKey);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}