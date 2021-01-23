namespace CookbookAPI.Models.Requests
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GoogleToken { get; set; }
    }
}
