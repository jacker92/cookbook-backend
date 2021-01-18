namespace CookbookAPI.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GoogleToken { get; set; }
    }
}
