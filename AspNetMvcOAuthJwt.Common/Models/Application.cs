namespace AspNetMvcOAuthJwt.Common.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}