namespace Cookbook_api.Models
{
    public class AuthenticateResponse
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
        }
    }
}
