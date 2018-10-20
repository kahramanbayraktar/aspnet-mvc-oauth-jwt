using AspNetMvcOAuthJwt.Common.Models;
using System.Collections.Generic;

namespace AspNetMvcOAuthJwt.Common.Helpers
{
    public class Constants
    {
        public static List<Application> Applications
        {
            get
            {
                return new List<Application>
                {
                    new Application {
                        ApplicationId = 1,
                        ClientId = "717ab38c7ecd41149f15f3e4acc0cabd",
                        ClientSecret = "DDPkrCgDcigOqbARTLIctWV2N4nxGDnoqBHZnMTZIg8"
                    },
                    new Application {
                        ApplicationId = 2,
                        ClientId = "717ab38c7ecd41149f15f3e4acc0cabe",
                        ClientSecret = "DDPkrCgDcigOqbARTLIctWV2N4nxGDnoqBHZnMTZIg9"
                    }
                };
            }
        }
    }
}