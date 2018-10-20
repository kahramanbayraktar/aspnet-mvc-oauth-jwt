using RestSharp;
using System.Web.Mvc;

namespace AspNetMvcOAuthJwt.Client.Controllers
{
    public class UserController : Controller
    {
        public IRestResponse Login(string email, string password)
        {
            var client = new RestClient("http://localhost/AspNetMvcOAuthJwt.AuthorizationServer"); // AuthorizationServer URL

            var request = new RestRequest("oauth2/token", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddObject(
                new
                {
                    username = email,
                    password = password,
                    grant_type = "password",
                    client_id = "717ab38c7ecd41149f15f3e4acc0cabd"
                });

            var response = client.Execute(request);
            return response;
        }
    }
}