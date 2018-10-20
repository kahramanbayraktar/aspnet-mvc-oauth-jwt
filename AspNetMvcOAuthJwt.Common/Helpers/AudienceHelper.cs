using AspNetMvcOAuthJwt.Common.Models;
using System.Linq;

namespace AspNetMvcOAuthJwt.Common.Helpers
{
    public class AudienceHelper
    {
        // These Application entities should be stored in database.
        public static Application FindAudience(string clientId)
        {
            var app = Constants.Applications.SingleOrDefault(a => a.ClientId == clientId);
            return app;
        }
    }
}